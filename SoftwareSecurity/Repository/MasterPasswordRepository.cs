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
    /// <summary>
    /// Repository for accessing and storing the master pw in db
    /// </summary>
    public class MasterPasswordRepository : IMasterPasswordRepository
    {
        private readonly PasswordManagerContext _context;

        public MasterPasswordRepository(PasswordManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds the master pw to the db
        /// </summary>
        /// <param name="masterPassword"></param>
        /// <returns></returns>
        public async Task AddMasterPasswordAsync(MasterPassword masterPassword)
        {
            _context.MasterPasswords.Add(masterPassword);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// retrieves the master pw from db
        /// </summary>
        /// <returns></returns>
        public async Task<MasterPassword> GetMasterPasswordAsync()
        {
            return await _context.MasterPasswords.FirstOrDefaultAsync();
        }
    }
}
