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
using Lingva.WebAPI.Dto;
using Lingva.DataAccessLayer.Exceptions;
using Lingva.BusinessLayer.Services;
using Lingva.DataAccessLayer;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{

    [Authorize]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserStatistic([FromRoute] int id)
        {

            var usersStatistics = await Task.Run(() => _statistics.GetUserGroups(id, DEFAULT_LIST_QUANTITY_COUNT));

            if (usersStatistics==null)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto());
            }
            UserStatisticsDto userStatistics = _mapper.Map<UserStatisticsDto>(usersStatistics);

            userStatistics.CreateSuccess();
            
            return Ok(userStatistics);
        }
    }
}
