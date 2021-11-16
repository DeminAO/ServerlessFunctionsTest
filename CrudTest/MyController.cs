using CrudTest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using Web.Core;

namespace CrudTest
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyController : ControllerBase
    {
        public ResourceResponse Get([FromServices] ExampleService exampleService)
            => exampleService.Get("some");

        [HttpGet("throw")]
        public ResourceResponse ThrowedGet([FromServices] ExampleService exampleService)
            => exampleService.Get(null);

    }

    public class ConfigurationStartup : ServerlessBase
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
            => services.AddSingleton<ExampleService>();

        public override void Configure(IApplicationBuilder app, ILogger logger)
        {
            app.Use(async (request, next) =>
            {
                try
                {
                    await next.Invoke();
                }
                catch (Exception e)
                {
                    var result = new ResourceResponseBase
                    {
                        IsSucceed = false,
                        Error = e.Message
                    };
                    string json = JsonConvert.SerializeObject(result);

                    await request.Response.WriteAsync(json);
                }
            });

        }
    }

    public class ExampleService
    {
        public ResourceResponse Get(string result)
        {
            return new ResourceResponse()
            {
                Result = new ModelResponse(result), 
                IsSucceed = true 
            };
        }
    }
}
