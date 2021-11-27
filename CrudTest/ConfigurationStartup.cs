using CrudTest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using Web.Core;

namespace CrudTest
{
	public class ConfigurationStartup : ServerlessBase
	{
		public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
			=> services
			.AddSingleton<ExampleService>()
			.AddSingleton(
				configuration
					.GetSection(nameof(TestConfig))
					.Get<TestConfig>());

		public override void Configure(IApplicationBuilder app, ILogger logger)
		{
			// add error handler
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
}
