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
using Microsoft.AspNetCore.Http;

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

        /// <summary>
        /// Authenticates user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /users/authenticate
        ///     {
        ///        "Id" : 1
        ///        "FirstName" : "firstName"
        ///        "LastName" : "lastName"
        ///        "UserName" : "userName"
        ///        "Password" : "password"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns OK if user authenticated</response>
        /// <param name="userDto">Info </param>
        /// <returns>Signed in user info</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate([FromBody]SignUpUserDto userDto)

        {
            User user;
            try
            {
                user = await Task.Run(() => _userService.Authenticate(userDto.Username, userDto.Password));
            }
            catch (UserServiceException ex)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto(ex.Message));
            }

            string tokenString = _userService.GetUserToken(user, _appSettings.Secret);

            SignInUserDto signInUser = _mapper.Map<SignInUserDto>(user);
            signInUser.Token = tokenString;

            signInUser.CreateSuccess();

            return Ok(signInUser);
        }

        /// <summary>
        /// Signs up user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /users/authenticate
        ///     {
        ///        "Id" : 1
        ///        "FirstName" : "string"
        ///        "LastName" : "translation"
        ///        "UserName" : "languageName"
        ///        "Password" : "password"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns OK if user signed up</response>
        /// <response code="404">If the exception handled</response>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Returns list of users.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Users
        ///     { }
        ///
        /// </remarks>
        /// <response code="200">Returns the users list</response>
        /// <response code="404">If the exception handled</response> 
        /// <returns>List of users</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var users = await Task.Run(() => _userService.GetAll());
            var userDtos = _mapper.Map<IList<SignUpUserDto>>(users);

            return Ok(userDtos);
        }

        /// <summary>
        /// Returns User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Users/{id}
        ///     { }
        ///
        /// </remarks>
        /// <response code="200">Returns the user</response>
        /// <response code="404">If the exception handled</response>
        /// <param name="id">id of requested user</param>
        /// <returns>Returns user info as user Dto</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            return await GetUserInfo(id);
        }

        /// <summary>
        /// Returns User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Users/me
        ///     { }
        ///
        /// </remarks>
        /// <response code="200">Returns the user</response>
        /// <response code="404">If the exception handled</response>
        /// <returns>Info of requested user as user Dto</returns>
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMyInfo()
        {
            return await GetUserInfo(UserService.GetLoggedInUserId(this));
        }

        /// <summary>
        /// Updates user info.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Dictionary
        ///     {
        ///        "Id" : 1
        ///        "FirstName" : "string"
        ///        "LastName" : "translation"
        ///        "UserName" : "languageName"
        ///        "Password" : "password"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns OK if user info updated</response>
        /// <response code="404">If the exception handled</response> 
        /// <param name="userDto"></param>
        /// <returns>Status of operation</returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody]SignUpUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = await Task.Run(() => UserService.GetLoggedInUserId(this));
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

        /// <summary>
        /// Deletes authenticated User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /user
        ///     { }
        ///
        /// </remarks>
        /// <response code="200">Returns OK if deleted</response>
        /// <response code="404">If exception is hendled</response>        
        /// <returns>Status of operation</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await Task.Run(() => _userService.Delete(UserService.GetLoggedInUserId(this)));

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
