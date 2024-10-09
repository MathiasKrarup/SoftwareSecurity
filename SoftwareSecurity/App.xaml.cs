using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoftwareSecurity.Repository;
using SoftwareSecurity.Repository.Interfaces;
using SoftwareSecurity.Services;
using SoftwareSecurity.Services.Interfaces;
using SoftwareSecurity.ViewModels;
using System;
using System.IO;
using System.Windows;

namespace SoftwareSecurity
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public App()
        {
            // Build configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            // Configure services
            var services = new ServiceCollection();
            ConfigureServices(services);

            // Build the service provider
            ServiceProvider = services.BuildServiceProvider();

            // Apply migrations on startup
            ApplyMigrations();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Get the connection string from configuration
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            // Register DbContext
            services.AddDbContext<PasswordManagerContext>(options =>
                options.UseSqlServer(connectionString));

            // Repositories
            services.AddScoped<IMasterPasswordRepository, MasterPasswordRepository>();

            // Services
            services.AddSingleton<IEncryptionService, EncryptionService>();
            services.AddSingleton<IMasterPasswordService, MasterPasswordService>();
            services.AddSingleton<ICredentialService, CredentialService>();

            // Windows (Views)
            services.AddTransient<LoginWindow>(provider =>
            {
                return new LoginWindow(
                    provider.GetRequiredService<IMasterPasswordService>(),
                    provider.GetRequiredService<IEncryptionService>(),
                    provider
                );
            });

            services.AddTransient<SetupMasterPasswordWindow>(provider =>
            {
                return new SetupMasterPasswordWindow(
                    provider.GetRequiredService<IMasterPasswordService>(),
                    provider.GetRequiredService<IEncryptionService>(),
                    provider
                );
            });

            services.AddTransient<MainWindow>(provider =>
            {
                return new MainWindow(
                    provider.GetRequiredService<ICredentialService>(),
                    provider
                );
            });

            services.AddTransient<AddCredentialView>(provider =>
            {
                return new AddCredentialView(
                    provider.GetRequiredService<ICredentialService>()
                );
            });

        }

        private void ApplyMigrations()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PasswordManagerContext>();
                dbContext.Database.EnsureCreated();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }

    }
}
