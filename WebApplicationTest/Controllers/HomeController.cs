using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("index");
        }

        [HttpGet("p")]
        public IActionResult Privacy()
        {
            return Ok("privacy");
        }
    }
}
