using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Repository
{
    public class PasswordManagerContextFactory : IDesignTimeDbContextFactory<PasswordManagerContext>
    {
        public PasswordManagerContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<PasswordManagerContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new PasswordManagerContext(optionsBuilder.Options);
        }

    }
}
