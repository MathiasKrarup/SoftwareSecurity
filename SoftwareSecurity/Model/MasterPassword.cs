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
        [Key]
        public int Id { get; set; }

        public string? Password { get; set; }

        public string? Salt { get; set; }
    }
}
