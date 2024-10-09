using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSecurity.Model
{
    public class Credential
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string? Website {  get; set; }

        [JsonProperty]
        public string? Username { get; set; }

        [JsonProperty]
        public string? Password { get; set; }


    }
}
