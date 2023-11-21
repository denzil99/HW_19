using HW19.BLL.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			//var date = DateTime.Today.AddDays(-1);

			//var readDirection = _configuration.GetValue<string>("ReadDirection");
			//var archiveDirection = _configuration.GetValue<string>("ArchiveDirection");

			//var raporMasterovDirection = _configuration.GetValue<string>("RaporttMasterovDirection");
			//var planProizvodstvaDirection = _configuration.GetValue<string>("PlanProizvodstvaDirection");


			_appLifetime.ApplicationStarted.Register(() =>
			{
				Task.Run(async () =>
				{
					try
					{
						using IServiceScope scope = _serviceProvider.CreateScope();

						IUserService reportsReaderService = scope.ServiceProvider.GetRequiredService<IUserService>();
						IMessageService transferFilesServise = scope.ServiceProvider.GetRequiredService<IMessageService>();

						Console.WriteLine("Войти в профиль (нажмите 1)");
						Console.WriteLine("Зарегистрироваться (нажмите 2)");

						switch (Console.ReadLine())
						{
							case "1":
								{
									Console.WriteLine("Введите почтовый адрес:");

									break;
								}

							case "2":
								{
									Program.registrationView.Show();
									break;
								}
						}

					}
					catch (Exception ex)
					{
						throw;
					}
					finally
					{
						// Stop the application once the work is done
						_appLifetime.StopApplication();
					}
				});
			});

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
		#endregion

	}
}
