using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly string[] Products = new[]
        {
            "Apple Laptop", "Apple Phone", "IPad", "Charger", "Monitor"
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Products);
        }
    }
}
