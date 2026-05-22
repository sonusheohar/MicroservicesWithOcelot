using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly string[] Users = new[]
        {
            "Sonu", "Ankit", "Lalit", "Bipin"
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Users);
        }
    }
}
