using SoftwareSecurity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Repository.Interfaces
{
    public interface IMasterPasswordRepository
    {
        /// <summary>
        /// Gets the master password
        /// </summary>
        /// <returns></returns>
        Task<MasterPassword> GetMasterPasswordAsync();
        /// <summary>
        /// Adds the MasterPassword
        /// </summary>
        /// <param name="masterPassword"></param>
        /// <returns></returns>
        Task AddMasterPasswordAsync(MasterPassword masterPassword);
    }
}
