using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Helpers
{
    public class PasswordHelper
    {
        public static byte[] GenerateSalt(int size = 16)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[size];
            rng.GetBytes(salt);
            return salt;
        }

        public static async Task<string> HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,

                DegreeOfParallelism = 8,
                Iterations = 4,
                MemorySize = 1024 * 64
            };

            byte[] hash = await argon2.GetBytesAsync(32);
            return Convert.ToBase64String(hash);
        }

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
