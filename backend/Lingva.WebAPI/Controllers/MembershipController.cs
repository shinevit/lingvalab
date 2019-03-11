using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


using AutoMapper;
using UsersManagementBackEnd.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using UsersManagementBackEnd.Services;
using UsersManagementBackEnd.Dtos;
using UsersManagementBackEnd.Model;
using UsersManagementBackEnd.Interfaces;

namespace UsersManagementBackEnd.Controllers
{
    [Route("api/[controller]")]
    public class MembershipController : Controller
    {
        private IMembersService _memberService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        [HttpPost("{groupID}/join")]
        public IActionResult JoinGroup([FromBody]UserDto userDto, int groupId)
        {
            // map dto to entity
            var user = _mapper.Map<User>(userDto);

            try
            {
                //get UserID   
                _memberService.JoinGroup(user.Id, groupId); 
                return Ok();
            }
            catch (LingvaException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{tokenString}/join+{groupID}")]
        public IActionResult JoinGroup( int groupId, string tokenString)
        {
            //get user.ID

            var token = new JwtSecurityToken(jwtEncodedString: tokenString);
            var user = new User { Id = Convert.ToInt32(token.Claims.First().Value) };

            try
            {
                _memberService.JoinGroup(user.Id, groupId);
                return Ok();
            }
            catch (LingvaException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{groupID}/leave")]
        public IActionResult LeaveGroup([FromBody]UserDto userDto, int groupId)
        {
            // map dto to entity
            var user = _mapper.Map<User>(userDto);

            try
            {
                // convert userDTO to User
                _memberService.LeaveGroup(user.Id, groupId);
                return Ok();
            }
            catch (LingvaException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{tokenString}/leave+{groupID}")]
        public IActionResult LeaveGroup(int groupId, string tokenString)
        {
            //get user.ID

            const string SECRET_KEY = "abcdf";
            SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

            var token = new JwtSecurityToken(jwtEncodedString: tokenString);            
            var user = new User { Id = Convert.ToInt32(token.Claims.First().Value) };

            try
            {               
                _memberService.LeaveGroup(user.Id, groupId);
                return Ok();
            }
            catch (LingvaException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _memberService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _memberService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
    }
}
