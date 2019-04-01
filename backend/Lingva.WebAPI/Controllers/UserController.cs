using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Entities;
using Lingva.WebAPI.Dto;
using Lingva.WebAPI.Helpers;
using Lingva.DataAccessLayer.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lingva.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

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
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var tokenString = _userService.GetUserToken(user, _appSettings.Secret);

            SignInUserDto signInUser = _mapper.Map<SignInUserDto>(user);
            signInUser.Token = tokenString;

            signInUser.CreateSuccess();

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

                return Ok(BaseStatusDto.CreateSuccessDto());
            }
            catch (UserServiceException ex)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto(ex.Message.ToString()));
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
            return await GetUserInfo(UserHelper.GetLoggedInUserId(this));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody]SignUpUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = await Task.Run(() => UserHelper.GetLoggedInUserId(this));
            try
            {
                await Task.Run(() => _userService.Update(user, userDto.Password));

                return Ok(BaseStatusDto.CreateSuccessDto());
            }
            catch (UserServiceException ex)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto(ex.Message.ToString()));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => _userService.Delete(id));

            return Ok(BaseStatusDto.CreateSuccessDto());
        }

        private async Task<IActionResult> GetUserInfo(int id)
        {
            var user = await Task.Run(() => _userService.GetById(id));
            var userDto = _mapper.Map<SignUpUserDto>(user);
            userDto.CreateSuccess();

            return Ok(userDto);
        }
    }
}