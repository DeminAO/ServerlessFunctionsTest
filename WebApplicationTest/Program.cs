using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;

namespace WebApplicationTest
{
	public class Program
	{
		public static void Main(string[] args)
		{
			IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args);

			builder
				.UseStartup<Startup>()
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					config
						.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
						.AddJsonFile("appsettings.json", false, true)
						.AddJsonFile($"appsettings.ext.json", true, true)
						.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
						.AddEnvironmentVariables();
				})
				.ConfigureServices(s =>
				{
					s.AddSingleton(builder);
				})
				.UseContentRoot(AppDomain.CurrentDomain.BaseDirectory);
			
			builder.Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			return WebHost
				.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
		}
	}
}