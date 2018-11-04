using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ePing.Api.dto
{


    [DataContract()]
    public class ListeClassementsEquipeHeader
    {
        [DataMember(Name = "liste")]
        [JsonProperty("liste")]
        public ClassementEquipeDtoHeader Liste { get; set; }

    }


    public class ClassementEquipeDtoHeader
    {

        [DataMember(Name = "classement")]
        [JsonProperty("classement")]
        public List<ClassementEquipeDto> Classements { get; set; }

    }
    public class ClassementEquipeDto
    {
        [DataMember(Name = "poule")]
        [JsonProperty("poule")]
        public string Poule{ get; set; }
        [DataMember(Name = "clt")]
        [JsonProperty("clt")]
        public string Classement { get; set; }
        [DataMember(Name = "equipe")]
        [JsonProperty("equipe")]
        public string Equipe { get; set; }
        [DataMember(Name = "joue")]
        [JsonProperty("joue")]
        public string NombreJoue { get; set; }
        [DataMember(Name = "pts")]
        [JsonProperty("pts")]
        public string PointRencontre { get; set; }
        [DataMember(Name = "numero")]
        [JsonProperty("numero")]
        public string Numero { get; set; }
    }
}
