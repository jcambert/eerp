using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Intranet.Api.dto
{
    [DataContract()]
    public class DelaiDto
    {
        [DataMember(Name = "delai")]
        [JsonProperty("delai")]
        public string Delai { get; set; }
    }
}
