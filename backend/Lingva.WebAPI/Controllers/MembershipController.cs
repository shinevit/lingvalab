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

        // POST: api/Todo
        /// <summary>
        /// Joins user into group.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /join/{groupID}
        ///     {
        ///        "id": 1
        ///     }
        ///
        /// </remarks>         
        [HttpPost("join/{groupID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> JoinGroup([FromBody]AuthenticateUserDto userDto, string groupID)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                user.Id = await Task.Run(() => UserService.GetLoggedInUserId(this));
                return Ok(await Task.Run(() => _groupService.JoinGroup(user.Id, int.Parse(groupID))));
            }
            catch (LingvaException)
            {
                return BadRequest() ;
            }
        }

        /// <summary>
        /// Deletes user from group.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1
        ///     }
        ///
        /// </remarks>
        [HttpDelete("leave/{groupID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> LeaveGroup([FromBody]AuthenticateUserDto userDto, int groupID)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = await Task.Run(() => UserService.GetLoggedInUserId(this));
            await Task.Run(() => _groupService.LeaveGroup(user.Id,groupID));
            return Ok();
        }
    }
}
