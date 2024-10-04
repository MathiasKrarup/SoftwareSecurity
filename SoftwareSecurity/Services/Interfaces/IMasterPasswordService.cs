using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Services.Interfaces
{
    public interface IMasterPasswordService
    {
        Task<bool> IsMasterPasswordSetAsync();
        Task<bool> ValidateMasterPasswordAsync(string password);
        Task SetMasterPasswordAsync(string password);
    }
}
