using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Model
{
    public class MasterPassword
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Hashed password for authentication
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Salt for password hashing
        /// </summary>
        public string? AuthSalt { get; set; }

        /// <summary>
        /// Salt for key derivation
        /// </summary>
        public string? KeySalt { get; set; }
    }
}
