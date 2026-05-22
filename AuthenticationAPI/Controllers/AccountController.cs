using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(IConfiguration config) : ControllerBase
    {
        private static ConcurrentDictionary<string, string> UserData = new ConcurrentDictionary<string, string>();

        //api/account/{email}/{password}
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            await Task.Delay(500);
            var getEmail = UserData!.Keys.Where(mod => mod.Equals(email)).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(getEmail))
            {
                UserData.TryGetValue(email, out string? dbPassword);
                if (!Equals(dbPassword, password))
                {
                    return BadRequest("Invalid Credential");
                }
                string jwtToken = GenerateToken(getEmail);
                return Ok(jwtToken);
            }
            return NotFound();
        }


        [HttpPost("register/{email}/{password}")]
        public async Task<ActionResult> Register(string email, string password)
        {
            await Task.Delay(500);
            var getEmail = UserData!.Keys.Where(mod => mod.Equals(email)).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(getEmail))
            {
                return BadRequest("User already exist");
            }
            UserData[email] = password;
            return Ok("Registration has been successfully!");
        }

        private string GenerateToken(string email)
        {
            var key = Encoding.UTF8.GetBytes(config["Authentication:Key"]!);
            var securityKey = new SymmetricSecurityKey(key);
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] { new Claim(ClaimTypes.Email, email!) };
            var token = new JwtSecurityToken(
                    issuer: config["Authentication:Issuer"],
                    audience: config["Authentication:Audience"],
                    claims: claims,
                    expires: null,
                    signingCredentials: credential
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
