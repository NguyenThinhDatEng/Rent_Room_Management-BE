using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.API.Controllers.Auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginParam login)
        {
            var authBL = new AuthBL();

            var result = authBL.ValidateLogin(login);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
