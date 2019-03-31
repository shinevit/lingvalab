using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Lingva.WebAPI.Helpers
{
    public class UserHelper
    {
        public static int GetLoggedInUserId(ControllerBase controllerBase)
        {
            return int.Parse(controllerBase.User.Claims.First(i => i.Type.Equals(ClaimTypes.Name))?.Value);
        }
    }
}