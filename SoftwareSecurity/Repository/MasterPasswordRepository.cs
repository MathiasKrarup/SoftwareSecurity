using Microsoft.EntityFrameworkCore;
using SoftwareSecurity.Model;
using SoftwareSecurity.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Repository
{
    public class MasterPasswordRepository : IMasterPasswordRepository
    {
        private readonly PasswordManagerContext _context;

        public MasterPasswordRepository(PasswordManagerContext context)
        {
            _context = context;
        }


        public async Task AddMasterPasswordAsync(MasterPassword masterPassword)
        {
            _context.MasterPasswords.Add(masterPassword);
            await _context.SaveChangesAsync();
        }

        public async Task<MasterPassword> GetMasterPasswordAsync()
        {
            return await _context.MasterPasswords.FirstOrDefaultAsync();
        }
    }
}
