using Microsoft.EntityFrameworkCore;
using SoftwareSecurity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Repository
{
    public class PasswordManagerContext : DbContext
    {
        public PasswordManagerContext(DbContextOptions<PasswordManagerContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Credential> Credentials { get; set; }
        public DbSet<MasterPassword> MasterPasswords { get; set; }
    }
}

