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
        }

        public DbSet<MasterPassword> MasterPasswords { get; set; }
    }
}

