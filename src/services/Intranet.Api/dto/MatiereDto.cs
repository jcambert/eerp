using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Intranet.Api.dto
{
    [DataContract()]
    public class MatiereDto
    {
        [DataMember(Name = "matiere")]
        [JsonProperty("matiere")]
        public string Matiere { get; set; }
        [DataMember(Name = "prixkg")]
        [JsonProperty("prixkg")]
        public double PrixKg { get;  set; }
    }
}
