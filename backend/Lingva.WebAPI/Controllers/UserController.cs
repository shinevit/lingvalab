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
using Lingva.DataAccessLayer;
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
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateUserDto userDto)
        {
            var user = await Task.Run(() => _userService.Authenticate(userDto.Username, userDto.Password));

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            string tokenString = _userService.GetUserToken(user, _appSettings.Secret);

            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]AuthenticateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            try
            {
                await Task.Run(() => _userService.Create(user, userDto.Password));
                return Ok();
            }
            catch (LingvaException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await Task.Run(() => _userService.GetAll());
            var userDtos = _mapper.Map<IList<AuthenticateUserDto>>(users);
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
        public async Task<IActionResult> Update([FromBody]AuthenticateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = await Task.Run(() => UserService.GetLoggedInUserId(this));
            try
            {
                await Task.Run(() => _userService.Update(user, userDto.Password));
                return Ok();
            }
            catch (LingvaException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => _userService.Delete(id));
            return Ok();
        }

        private async Task<IActionResult> GetUserInfo(int id)
        {
            var user = await Task.Run(() => _userService.GetById(id));
            var userDto = _mapper.Map<AuthenticateUserDto>(user);
            return Ok(userDto);
        }

    }
}
