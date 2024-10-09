using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Services.Interfaces
{
    public interface IEncryptionService
    {
        Task InitializeAsync(string masterPassword);
        byte[] Key { get; }
    }
}
