using HW19.BLL.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HW19.BLL.Services
{
	public class InitializationService : IHostedService
	{
		#region Data
		private readonly IHostApplicationLifetime _appLifetime;
		private readonly IServiceProvider _serviceProvider;
		#endregion

		#region .ctor
		public InitializationService(
			IHostApplicationLifetime appLifetime,
			IServiceProvider serviceProvider
			)
		{
			_appLifetime = appLifetime;
			_serviceProvider = serviceProvider;

		}
		#endregion

		#region Public
		public Task StartAsync(CancellationToken cancellationToken)
		{
			Console.WriteLine("__________________________");
			try
			{
				_appLifetime.ApplicationStarted.Register(() =>
					{
						Task.Run(async () =>
						{
							try
							{
								// Да, через вьюхи это определенно делается проще
								// Но у самурая нет цели, есть только путь
								// Ну а если серьёзно, то на работе используем слоёную архитектуру, и нужно было
								// дополнительно попрактиковаться с DI
								// По факту особо и не вышло
								using IServiceScope scope = _serviceProvider.CreateScope();

								IUserInterfaceService userInterfaceService = scope.ServiceProvider.GetRequiredService<IUserInterfaceService>();

								await userInterfaceService.Run();
							}
							catch (Exception)
							{
								throw;
							}
							finally
							{
								_appLifetime.StopApplication();
							}
						});
					});
			}
			catch (Exception)
			{

				throw;
			}
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
		#endregion

	}
}
