using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Intranet.Api.dto
{
    [DataContract()]
    public class TypeMatiereDto
    {
        [DataMember(Name = "typematiere")]
        [JsonProperty("typematiere")]
        public string TypeMatiere { get; set; }
        [DataMember(Name = "densite")]
        [JsonProperty("densite")]
        public double Densite { get;  set; }
    }
}
