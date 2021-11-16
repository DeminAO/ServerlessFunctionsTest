using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using Web.Core;

namespace WebApplicationTest
{
	public class Startup
	{
		Assembly assembly;
		Type type;
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			try
			{
				var dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules", "CrudTest.dll");
				assembly = Assembly.LoadFile(dir);
				type = assembly.GetType("CrudTest.ConfigurationStartup");
				if (type == null)
                {
					Console.WriteLine("module not found");
				}
			}
            catch (Exception e)
            {
				Console.WriteLine("module not found");
            }
		}

		public IConfiguration Configuration { get; }
		ServerlessBase serverless;

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton(type);
			serverless = services.BuildServiceProvider().GetRequiredService(type) as ServerlessBase;

			services
				.AddControllers()
				.AddApplicationPart(assembly);

			serverless.ConfigureServices(services, Configuration);
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

			serverless.Configure(app, logger);

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
