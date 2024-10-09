using SoftwareSecurity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Services.Interfaces
{
    public interface ICredentialService
    {
        /// <summary>
        /// Retrieves all credentials
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Credential>> GetAllCredentialsAsync();
        /// <summary>
        /// Retrieve a credential by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Credential> GetCredentialByIdAsync(int id);
        /// <summary>
        /// Adds a new credential
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        Task AddCredentialAsync(Credential credential);
        /// <summary>
        /// Updates an existing credential
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        Task UpdateCredentialAsync(Credential credential);
        /// <summary>
        /// Deletes a credential by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteCredentialAsync(int id);
    }
}
