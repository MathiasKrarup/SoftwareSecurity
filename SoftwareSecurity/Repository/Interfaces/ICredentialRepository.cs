using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareSecurity.Model;

namespace SoftwareSecurity.Repository.Interfaces
{
    public interface ICredentialRepository
    {
        /// <summary>
        /// Gets all the credentials
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Credential>> GetAllCredentialsAsync();
        /// <summary>
        /// Gets credential by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Credential> GetCredentialByIdAsync(int id);
        /// <summary>
        /// Add new credentials
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        Task AddCredentialAsync(Credential credential);
        /// <summary>
        /// Update existing credentials
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        Task UpdateCredentialAsync(Credential credential);
        /// <summary>
        /// Delete stored credentials
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteCredentialAsync(int id);
    }
}
