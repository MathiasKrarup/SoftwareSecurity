using SoftwareSecurity.Helpers;
using SoftwareSecurity.Repository.Interfaces;
using SoftwareSecurity.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Services
{
    /// <summary>
    /// Service responsible for deriving the encryption from the master pw
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        private readonly IMasterPasswordRepository _masterPasswordRepository;
        private byte[] _key;


        public byte[] Key => _key;


        public EncryptionService(IMasterPasswordRepository masterPasswordRepository)
        {
            _masterPasswordRepository = masterPasswordRepository;
        }

        /// <summary>
        /// Derives the encryption key from the masterpw
        /// </summary>
        /// <param name="masterPassword"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task InitializeAsync(string masterPassword)
        {
            var masterPasswordRecord = await _masterPasswordRepository.GetMasterPasswordAsync();
            if (masterPasswordRecord == null)
                throw new Exception("Master password not set.");

            var keySaltBytes = Convert.FromBase64String(masterPasswordRecord.KeySalt);

            // Derive the encryption key from the master password and keySalt
            _key = PasswordHelper.DeriveKey(masterPassword, keySaltBytes);
        }
    }
}
