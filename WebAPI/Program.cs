using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;
using System;

namespace WebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var hostserver = CreateHostBuilder(args).Build();
			using (var ambiente = hostserver.Services.CreateScope())
			{
				var services = ambiente.ServiceProvider;
				try
				{
					var context = services.GetRequiredService<CursosOnlineContext>();
					context.Database.Migrate();
				}
				catch (Exception e)
				{
					var loggin = services.GetRequiredService<ILogger<Program>>();
					loggin.LogError(e, "Ocurrió un error en la migración");
				}

			}
			hostserver.Run();
			//   CreateHostBuilder(args).Build().Run();

		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
