using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

namespace Lingva.WebAPI.Controllers
{ 
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MembershipController : ControllerBase
    {
        private IGroupService _groupService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public MembershipController(
            IGroupService groupService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _groupService = groupService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        // POST: /Membership
        /// <summary>
        /// Joins user into group.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /join/{groupID}
        ///     {
        ///        "Id" : 1
        ///        "FirstName" : string
        ///        "LastName" : string
        ///        "Username" : string
        ///        "Token" : string
        ///     }
        ///
        /// </remarks> 
        /// /// <param name="groupID"></param>
        /// <returns>Joining complete</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="404">If the item is null</response> 
        [HttpPost("join/{groupID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> JoinGroup([FromBody]SignInUserDto userDto, [FromRoute] int groupID)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                user.Id = await Task.Run(() => UserService.GetLoggedInUserId(this));
                return Ok(BaseStatusDto.CreateSuccessDto());
            }
            catch (LingvaException)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto()) ;
            }
        }

        /// <summary>
        /// Deletes user from group.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /leave/{groupID}
        ///     {
        ///        "Id" : 1
        ///        "FirstName" : string
        ///        "LastName" : string
        ///        "Username" : string
        ///        "Token" : string
        ///     }
        ///
        /// </remarks>
        /// <param name="groupID"></param>
        /// <returns>Leaving complete</returns>
        /// <response code="200">Returns status</response>
        /// <response code="404">If the exception is handled</response> 
        [HttpDelete("leave/{groupID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> LeaveGroup([FromBody]SignInUserDto userDto, [FromBody] int groupID)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                await Task.Run(() => _groupService.LeaveGroup(user.Id, groupID));
                return Ok(BaseStatusDto.CreateSuccessDto());
            }
            catch (LingvaException)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto());
            }
        }
    }
}
