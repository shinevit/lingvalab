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
using Lingva.DataAccessLayer.Exceptions;
using Lingva.BusinessLayer.Services;
using Lingva.DataAccessLayer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Lingva.WebAPI.Controllers
{
    [Authorize]
    [ApiController]   
    public class StatisticsController : ControllerBase
    {       
        private IMapper _mapper;
        private IStatisticsService _statistics;
        
        public StatisticsController(IMapper mapper, IStatisticsService statistics)
        {
            _mapper = mapper;
            _statistics = statistics;
        }

        /// <summary>
        /// Returns groups of requested user
        /// </summary>
        /// <remarks>
        /// 
        ///     GET: Statics/user/{id}/groups
        /// 
        /// </remarks>
        /// <param name="userId">Id of requested user</param>
        /// <response code="200">Returns status and user`s groups</response>
        /// <response code="404">If the exception is handled</response>
        /// <returns></returns>
        [HttpGet("user/{id}/groups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserStatistic([FromRoute] int userId)
        {
            var usersStatistics = await Task.Run(() => _statistics.GetUserGroups(userId));

            if (usersStatistics == null)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto());
            }

            var userStatistics = _mapper.Map<IList<GroupViewDTO>>(usersStatistics);

            return Ok(userStatistics);
        }

        /// <summary>
        /// Returns user in requested group
        /// </summary>
        /// <remarks>
        /// 
        ///     GET: Statics/groups/{id}/users
        /// 
        /// </remarks>
        /// <param name="groupId">Id of requested group</param>
        /// <response code="200">Returns status and users in group</response>
        /// <response code="404">If the exception is handled</response>
        /// <returns>Users in requested group</returns>
        [HttpGet("groups/{id}/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetGroupUsers([FromRoute] int groupId)
        {
            var groupStatistics = await Task.Run(() => _statistics.GetGroupParticipants(groupId));

            if (groupStatistics == null)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto());
            }

            var groupsUsers = _mapper.Map<IList<UserGroupsDTO>>(groupStatistics);

            return Ok(groupsUsers);
        }
    }
}

