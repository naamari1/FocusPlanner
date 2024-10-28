using FocusPlanner.Core.Interfaces;
using FocusPlanner.Infastructure.Data_Access;
using FocusPlanner.Infastructure.Repositories;
using FocusPlanner.Notification;
using FocusPlanner.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Licensing;
using System.IO;
using System.Windows;

namespace FocusPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            SyncfusionLicenseProvider.RegisterLicense("MzUwNzU1N0AzMjM3MmUzMDJlMzBvRDV1V2V2NmlxQlRQaVFGM2MrU1VuOTdHaktmN2NnSGYvdmhKRzAvMUpNPQ==");

        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Add DbContext and configure SQL Server
            services.AddDbContext<TaskDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            // Register other services and repositories here
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<NotificationService>();

            services.AddScoped<MainViewModel>();


            // Register MainWindow to be resolved by the service provider
            services.AddSingleton<MainWindow>();
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            if (mainWindow != null)
            {
                mainWindow.Show();
            }
            else
            {
                // Consider logging this situation or handling it appropriately
                throw new InvalidOperationException("MainWindow could not be resolved. Please check your service registrations.");
            }
        }

    }

}
