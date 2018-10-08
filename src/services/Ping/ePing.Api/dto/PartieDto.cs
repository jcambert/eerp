using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ePing.Api.dto
{
    [DataContract]
    public class ListePartiesHeader
    {
        [DataMember(Name = "liste")]
        [JsonProperty("liste")]
        public PartiesDtoHeader Liste { get; set; }
    }

    public class PartiesDtoHeader
    {
        [DataMember(Name = "resultat")]
        [JsonProperty("resultat")]
        public List<PartieDto> Parties { get; set; }

    }
    public class PartieDto
    {
        [DataMember(Name = "date")]
        [JsonProperty("date")]
        public string  Date{ get; set; }
        [DataMember(Name = "nom")]
        [JsonProperty("nom")]
        public string Nom { get; set; }
        [DataMember(Name = "classement")]
        [JsonProperty("classement")]
        public double Classement { get; set; }
        [DataMember(Name = "epreuve")]
        [JsonProperty("epreuve")]
        public string Epreuve { get; set; }
        [DataMember(Name = "victoire")]
        [JsonProperty("victoire")]
        public string Victoire { get; set; }
        [DataMember(Name = "forfait")]
        [JsonProperty("forfait")]
        public string Forfait{ get; set; }
        [DataMember(Name = "idpartie")]
        [JsonProperty("idpartie")]
        public string IdPartie { get; set; }
    }
}
