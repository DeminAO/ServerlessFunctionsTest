using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using Web.Core;

namespace WebApplicationTest
{
	public class Startup
	{
		private readonly IConfiguration configuration;

		private readonly Assembly assembly;
		private readonly Type configType;
		private ServerlessBase serverless;

		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
			try
			{
				var moduleName = configuration.GetValue<string>("Module");

				var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{moduleName}.dll");
				assembly = Assembly.LoadFile(dir);
				configType = assembly.GetType($"{moduleName}.ConfigurationStartup");
			}
			catch
			{
				Console.WriteLine("module not found");
			}
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "ASP0000")]
		public void ConfigureServices(IServiceCollection services)
		{
			if (assembly != null)
			{
				services
					.AddControllers()
					.AddApplicationPart(assembly);
			}

			if (configType != null)
			{
				// add as singleton
				services.AddSingleton(configType);
				// get with injected services
				serverless = services.BuildServiceProvider().GetService(configType) as ServerlessBase;
				// call configuration
				serverless.ConfigureServices(services, configuration);
			}
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseRouting();

			serverless?.Configure(app, logger);

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}