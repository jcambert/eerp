using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Intranet.Api.dto
{
    [DataContract()]
    public class OperationDto
    {
        [DataMember(Name = "nom")]
        [JsonProperty("nom")]
        public string Nom { get; set; }
        [DataMember(Name = "aide")]
        [JsonProperty("aide")]
        public string Aide { get; set; } = "";
        [DataMember(Name = "txprep")]
        [JsonProperty("txprep")]
        public int TauxHorrairePreparation { get; set; }
        [DataMember(Name = "tpsprep")]
        [JsonProperty("tpsprep")]
        public double TempsPreparation { get; set; }
        [DataMember(Name = "txope")]
        [JsonProperty("txope")]
        public int TauxHorraireOperation { get; set; }
        [DataMember(Name = "tpsope")]
        [JsonProperty("tpsope")]
        public double TempsOperation { get; set; }
    }
}
