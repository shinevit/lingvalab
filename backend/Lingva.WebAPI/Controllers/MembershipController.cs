using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using UsersManagementBackEnd.Services;
using Lingva.DataAccessLayer.Entities;
namespace UsersManagementBackEnd.Controllers
{
    [Route("api/[controller]")]
    public class MembershipController : Controller
    {
        private EventMemberService _memberService = new EventMemberService();

        [HttpPost("join/{groupID}/{tokenString}")]
        public IActionResult JoinGroup( int groupID,string tokenString)
        {
            //getting user.ID
            var token = new JwtSecurityToken(jwtEncodedString: tokenString);
            var user = new User { Id = Convert.ToInt32(token.Claims.First().Value) };

            try
            {
                _memberService.JoinGroup(user.Id, groupID);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("leave/{groupID}/{tokenString}")]
        public IActionResult LeaveGroup(int groupID, string tokenString)
        {
            //getting user.ID
            var token = new JwtSecurityToken(jwtEncodedString: tokenString);            
            var user = new User { Id = Convert.ToInt32(token.Claims.First().Value) };

            try
            {               
                _memberService.LeaveGroup(user.Id, groupID);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }        
    }
}
