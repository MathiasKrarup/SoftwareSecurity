using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoftwareSecurity.Repository;
using SoftwareSecurity.Repository.Interfaces;
using SoftwareSecurity.Services.Interfaces;
using SoftwareSecurity.Services;
using System.Configuration;
using System.Data;

using System.IO;
using System.Windows;

namespace SoftwareSecurity
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public App()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<PasswordManagerContext>(options =>
                options.UseSqlServer(connectionString));


            // Repos
            services.AddScoped<IMasterPasswordRepository, MasterPasswordRepository>();
            services.AddScoped<ICredentialRepository, CredentialRepository>();

            // Services
            services.AddScoped<IMasterPasswordService, MasterPasswordService>();
            services.AddScoped<ICredentialService, CredentialService>();

            // viewmodels
            services.AddTransient<MainWindow>();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }

    }

}
