using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using Lingva.BusinessLayer.Contracts;
using Lingva.WebAPI.Dto;
using Lingva.WebAPI.Helpers;
using Lingva.DataAccessLayer.Entities;
using Lingva.BusinessLayer.Services;
using Lingva.DataAccessLayer.Exceptions;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private BaseStatusDto _responseDto = new BaseStatusDto();

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]SignUpUserDto userDto)
        {
            var user = await Task.Run(() => _userService.Authenticate(userDto.Username, userDto.Password));

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            string tokenString = _userService.GetUserToken(user, _appSettings.Secret);

            SignInUserDto signInUser = _mapper.Map<SignInUserDto>(user);
            signInUser.Token = tokenString;

            signInUser.CreateSucess();

            return Ok(signInUser);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]SignUpUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            try
            {
                await Task.Run(() => _userService.Create(user, userDto.Password));
                _responseDto.CreateSucess();

                return Ok(_responseDto);
            }
            catch (UserServiceException ex)
            {
                _responseDto.CreateError(ex.Message);

                return BadRequest(_responseDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await Task.Run(() => _userService.GetAll());
            var userDtos = _mapper.Map<IList<SignUpUserDto>>(users);
            
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return await GetUserInfo(id);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyInfo()
        {
            return await GetUserInfo(UserService.GetLoggedInUserId(this));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody]SignUpUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = await Task.Run(() => UserService.GetLoggedInUserId(this));
            try
            {
                await Task.Run(() => _userService.Update(user, userDto.Password));
                _responseDto.CreateSucess();

                return Ok(_responseDto);
            }
            catch (UserServiceException ex)
            {
                _responseDto.CreateError(ex.Message);

                return BadRequest(_responseDto);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => _userService.Delete(id));
            _responseDto.CreateSucess();

            return Ok(_responseDto);
        }

        private async Task<IActionResult> GetUserInfo(int id)
        {
            var user = await Task.Run(() => _userService.GetById(id));
            var userDto = _mapper.Map<SignUpUserDto>(user);
            userDto.CreateSucess();

            return Ok(userDto);
        }

    }
}
