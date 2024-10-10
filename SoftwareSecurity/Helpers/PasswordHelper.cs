using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Helpers
{
    /// <summary>
    /// Provides helper methods for password hashing and key derivation
    /// </summary>
    public class PasswordHelper
    {
        /// <summary>
        /// Generates a secure random salt
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] GenerateSalt(int size = 16)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[size];
            rng.GetBytes(salt);
            return salt;
        }

        /// <summary>
        /// Hashes a password using Argon2id
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static async Task<string> HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,

                DegreeOfParallelism = 1,
                Iterations = 4,
                MemorySize = 9216
            };

            byte[] hash = await argon2.GetBytesAsync(32);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Verifies password against stored hash
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="storedHash"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static async Task<bool> VerifyPassword(string enteredPassword, string storedHash, byte[] salt)
        {
            string hashOfEnteredPassword = await HashPassword(enteredPassword, salt);
            return hashOfEnteredPassword == storedHash;
        }


        // Uses PBKDF2 with MHACSHA256 to derive key
        public static byte[] DeriveKey(string password, byte[] salt, int keyLength = 32)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(keyLength);
        }


    }
}
