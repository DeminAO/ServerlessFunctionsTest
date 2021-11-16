using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.Core;

namespace CrudTest
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok("Get");
        }
    }

    public class ConfigurationStartup : ServerlessBase
    {
	}
}
