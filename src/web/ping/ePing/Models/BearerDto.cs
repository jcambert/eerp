using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ePing.Models
{
    [DataContract]
    public class BearerDto
    {
        [DataMember(Name ="jwt")]
        [JsonProperty("jwt")]
        public string Jwt { get; set; }
    }
}
