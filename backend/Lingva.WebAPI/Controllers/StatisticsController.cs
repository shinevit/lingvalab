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
    public class StatisticsController : ControllerBase
    {
        private IMapper _mapper;
        private IStatisticsService _statistics;
        private IUserService _userService;

        public StatisticsController(IMapper mapper, IStatisticsService statistics, IUserService userService)
        {
            _mapper = mapper;
            _statistics = statistics;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserStatistic([FromRoute] int id)
        {
            var user = await Task.Run(() => _userService.GetById(id));

        }
    }
}
