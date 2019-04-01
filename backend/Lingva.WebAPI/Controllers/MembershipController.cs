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
using Lingva.DataAccessLayer.Exceptions;

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
        /// <returns>Joining complete</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="404">If the item is null</response> 
        [HttpPost("join/{groupID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> JoinGroup([FromRoute] int groupID)
        {
            try
            {
                await Task.Run(() => _groupService.JoinGroup(UserService.GetLoggedInUserId(this), groupID));
            }
            catch (LingvaException)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto());
            }
            catch (Exception ex)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto(ex.Message));
            }

            return Ok(BaseStatusDto.CreateSuccessDto());
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
        /// <returns>Leaving complete</returns>
        /// <response code="200">Returns status</response>
        /// <response code="404">If the exception is handled</response> 
        [HttpDelete("leave/{groupID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> LeaveGroup([FromRoute] int groupID)
        {
            try
            {
                await Task.Run(() => _groupService.LeaveGroup(UserService.GetLoggedInUserId(this), groupID));
                return Ok(BaseStatusDto.CreateSuccessDto());
            }
            catch (LingvaException ex)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto(ex.Message));
            }
        }
    }
}

