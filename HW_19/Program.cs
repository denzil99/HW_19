using HW19.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace HW_19
{
	class Program
	{
		private static async Task Main(string[] args)
		{
			var configuration = new ConfigurationBuilder()
	   .AddEnvironmentVariables()
	   .AddCommandLine(args)
	   .AddJsonFile("appsettings.json")
	   .Build();

			await Host.CreateDefaultBuilder(args)
				.UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
				.ConfigureAppConfiguration(builder =>
				{
					builder.Sources.Clear();
					builder.AddConfiguration(configuration);
				})
				.ConfigureLogging((host, logging) =>
				{
					
				})
				.ConfigureServices((hostContext, services) =>
				{
					try
					{
						services.AddHostedService<InitializationService>();
						services.AddScoped<IReportsReaderService, ReportsReaderService>();
						services.AddScoped<ITransferFilesServise, TransferFilesServise>();
						services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql("name=ConnectionStrings:DefaultConnection"));
					}
					catch (Exception)
					{
						throw;
					}
				})
				.RunConsoleAsync();
		}
	}
}