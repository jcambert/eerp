using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ePing.Api.dto
{
    [DataContract()]
    public class ListeEquipeHeader
    {
        [DataMember(Name = "liste")]
        [JsonProperty("liste")]
        public EquipeDtoHeader Liste { get; set; }

    }


    public class EquipeDtoHeader
    {

        [DataMember(Name = "equipe")]
        [JsonProperty("equipe")]
        public List<EquipeDto> Equipes{ get; set; }

    }


    public class EquipeDto
    {
        [DataMember(Name = "libequipe")]
        [JsonProperty("libequipe")]
        public string Libelle { get; set; }
        [DataMember(Name = "libdivision")]
        [JsonProperty("libdivision")]
        public string Division { get; set; }
        [DataMember(Name = "liendivision")]
        [JsonProperty("liendivision")]
        public string LienDivision { get; set; }
        [DataMember(Name = "idepr")]
        [JsonProperty("idepr")]
        public int IdEpreuve { get; set; }
        [DataMember(Name = "libepr")]
        [JsonProperty("libepr")]
        public string Epreuve { get; set; }
    }
}
