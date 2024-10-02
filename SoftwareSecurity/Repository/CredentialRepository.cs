using SoftwareSecurity.Model;
using SoftwareSecurity.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Repository
{
    public class CredentialRepository : ICredentialRepository
    {
        public Task AddCredentialAsync(Credential credential)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCredentialAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Credential>> GetAllCredentialsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Credential> GetCredentialByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCredentialAsync(Credential credential)
        {
            throw new NotImplementedException();
        }
    }
}
