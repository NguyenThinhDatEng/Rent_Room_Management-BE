using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentRoomManagement.BL;
using RentRoomManagement.Common.Param;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentRoomManagement.API.Controllers.Auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginParam login)
        {
            var authBL = new AuthBL();

            var user = await authBL.ValidateLogin(login);

            if (user == null)
            {
                return BadRequest();
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.user_id.ToString()),
                new Claim("r", login.Role.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireAt = DateTime.UtcNow.AddMinutes(120);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: expireAt, signingCredentials: signIn);

            string tokenVal = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenVal,
                User = user,
                ExpireAt = expireAt,
                Role = (int)login.Role,
            });
        }
    }
}
