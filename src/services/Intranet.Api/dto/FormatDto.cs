using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Intranet.Api.dto
{
    [DataContract()]
    public class FormatDto
    {
        [DataMember(Name = "longueur")]
        [JsonProperty("longueur")]
        public int Longueur { get; set; }
        [DataMember(Name = "largeur")]
        [JsonProperty("largeur")]
        public int Largeur { get; set; }
    }
}
