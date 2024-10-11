using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Helpers
{
    /// <summary>
    /// Provides helper methods for encryption and decryption using AES
    /// </summary>
    public class EncryptionHelper
    {

        /// <summary>
        /// Generates a random IV
        /// </summary>
        /// <param name="size">Size of the IV</param>
        /// <returns></returns>
        public static byte[] GenerateRandomIV(int size = 16)
        {
            byte[] iv = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(iv);
            }
            return iv;
        }



        /// <summary>
        /// Encrypts a plaintext string using AES encryption with a given key and IV
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// Decrypts the encrypted string using AES with given key and IV
        /// </summary>
        /// <param name="cipher">Base64-encoded encrypted data</param>
        /// <param name="key">The decryption key</param>
        /// <param name="iv">Initialization vector</param>
        /// <returns></returns>
        public static string DecryptString(string cipher, byte[] key, byte[] iv) 
        {
            var buffer = Convert.FromBase64String(cipher);

            using (var aes = Aes.Create()) 
            {
                aes.Key = key;
                aes.IV = iv;

                using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using var ms = new MemoryStream(buffer);
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var sr = new StreamReader(cs);
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}

