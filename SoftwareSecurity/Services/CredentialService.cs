using Newtonsoft.Json;
using SoftwareSecurity.Helpers;
using SoftwareSecurity.Model;
using SoftwareSecurity.Repository.Interfaces;
using SoftwareSecurity.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Services
{
    public class CredentialService : ICredentialService
    {
        private readonly IEncryptionService _encryptionService;
        private readonly string _credentialFilePath = "credentials.dat";



        public CredentialService(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        private async Task<List<Credential>> LoadCredentialAsync()
        {
            if (File.Exists(_credentialFilePath))
            {
                byte[] fileData = await File.ReadAllBytesAsync(_credentialFilePath);


                byte[] iv = new byte[16];
                Array.Copy(fileData, 0, iv, 0, iv.Length);


                byte[] encryptedData = new byte[fileData.Length - iv.Length];
                Array.Copy(fileData, iv.Length, encryptedData, 0, encryptedData.Length);

                string decryptedJson = EncryptionHelper.DecryptString(
                    Convert.ToBase64String(encryptedData),
                    _encryptionService.Key,
                    iv);

                return JsonConvert.DeserializeObject<List<Credential>>(decryptedJson) ?? new List<Credential>();
            }
            else
            {
                return new List<Credential>();
            }
        }

        private async Task SaveCredentialsAsync(List<Credential> credentials)
        {
            string json = JsonConvert.SerializeObject(credentials);

            byte[] iv = EncryptionHelper.GenerateRandomIV();

            string encryptedData = EncryptionHelper.EncryptString(json, _encryptionService.Key, iv);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

            using (var fs = new FileStream(_credentialFilePath, FileMode.Create))
            {
                await fs.WriteAsync(iv, 0, iv.Length);
                await fs.WriteAsync(encryptedBytes, 0, encryptedBytes.Length);
            }
        }

        public async Task AddCredentialAsync(Credential credential)
        {
            var credentials = await LoadCredentialAsync();

            credential.Id = credentials.Count > 0 ? credentials[^1].Id + 1 : 1;

            credentials.Add(credential);

            await SaveCredentialsAsync(credentials);
        }

        public async Task DeleteCredentialAsync(int id)
        {
            var credentials = await LoadCredentialAsync();

            var credential = credentials.Find(c => c.Id == id);
            if (credential != null)
            {
                credentials.Remove(credential);
                await SaveCredentialsAsync(credentials);
            }
        }

        public async Task<IEnumerable<Credential>> GetAllCredentialsAsync()
        {
            var credentials = await LoadCredentialAsync();
            return credentials;
        }

        public async Task<Credential> GetCredentialByIdAsync(int id)
        {
            var credentials = await LoadCredentialAsync();
            return credentials.Find(c => c.Id == id);
        }

        public async Task UpdateCredentialAsync(Credential credential)
        {
            var credentials = await LoadCredentialAsync();

            var existingCredential = credentials.Find(c => c.Id == credential.Id);
            if (existingCredential != null)
            {
                existingCredential.Website = credential.Website;
                existingCredential.Username = credential.Username;
                existingCredential.Password = credential.Password;

                await SaveCredentialsAsync(credentials);
            }
        }
    }
}