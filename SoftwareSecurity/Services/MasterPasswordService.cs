using SoftwareSecurity.Helpers;
using SoftwareSecurity.Model;
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
    /// Service for managing the masterpw
    /// </summary>
    public class MasterPasswordService : IMasterPasswordService
    {
        private readonly IMasterPasswordRepository _MasterPwRepository;

        public MasterPasswordService(IMasterPasswordRepository repository)
        {
            _MasterPwRepository = repository;
        }

        /// <summary>
        /// Checks if the master pw is set
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsMasterPasswordSetAsync()
        {
            var masterPassword = await _MasterPwRepository.GetMasterPasswordAsync();
            return masterPassword != null;
        }

        /// <summary>
        /// Sets the master password by hashing it and storing the hash and salt
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task SetMasterPasswordAsync(string password)
        {
            byte[] authSalt = PasswordHelper.GenerateSalt();
            string hashedPassword = await PasswordHelper.HashPassword(password, authSalt);

            byte[] keySalt = PasswordHelper.GenerateSalt();

            var masterPassword = new MasterPassword
            {
                Password = hashedPassword,
                AuthSalt = Convert.ToBase64String(authSalt),
                KeySalt = Convert.ToBase64String(keySalt)
            };

            await _MasterPwRepository.AddMasterPasswordAsync(masterPassword);
        }

        /// <summary>
        /// Validates the entered master password agaisnt the stored hash
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> ValidateMasterPasswordAsync(string password)
        {
            var masterPassword = await _MasterPwRepository.GetMasterPasswordAsync();
            if (masterPassword == null)
            {
                return false;
            }

            byte[] authSalt = Convert.FromBase64String(masterPassword.AuthSalt);
            return await PasswordHelper.VerifyPassword(password, masterPassword.Password, authSalt);
        }


    }
}