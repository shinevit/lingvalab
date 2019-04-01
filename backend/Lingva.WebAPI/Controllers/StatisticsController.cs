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

namespace Lingva.WebAPI.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        private const int DEFAULT_LIST_QUANTITY_COUNT = 10;

        private IMapper _mapper;
        private IStatisticsService _statistics;


        public StatisticsController(IMapper mapper, IStatisticsService statistics)
        {
            _mapper = mapper;
            _statistics = statistics;
        }

        [HttpGet("user/{id}/groups")]
        public async Task<IActionResult> GetUserStatistic([FromRoute] int userId)
        {
            var usersStatistics = await Task.Run(() => _statistics.GetUserGroups(userId, DEFAULT_LIST_QUANTITY_COUNT));

            if (usersStatistics == null)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto());
            }

            var userStatistics = _mapper.Map<IList<UserGroupsDTO>>(usersStatistics);

            return Ok(userStatistics);
        }

        [HttpGet("groups/{id}/users")]
        public async Task<IActionResult> GetGroupUsers([FromRoute] int groupId)
        {
            var groupStatistics = await Task.Run(() => _statistics.GetGroupParticipants(groupId, DEFAULT_LIST_QUANTITY_COUNT));

            if (groupStatistics == null)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto());
            }

            var groupsUsers = _mapper.Map<IList<UserGroupsDTO>>(groupStatistics);

            return Ok(groupsUsers);
        }
    }
}

