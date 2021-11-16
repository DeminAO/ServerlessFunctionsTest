using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Core;

namespace CrudTest
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyController : ControllerBase
    {
        public IActionResult Get([FromServices] ExampleService exampleService)
        {
            return Ok(exampleService.Get);
        }
    }

    public class ConfigurationStartup : ServerlessBase
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ExampleService>();

            base.ConfigureServices(services, configuration);
        }
    }

    public class ExampleService
    {
        public string Get => "example string";
    }
}
