using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ePing.Api.dto
{
   
    [DataContract]
    public class ListeClassementHeader
    {
        [DataMember(Name = "liste")]
        [JsonProperty("liste")]
        public ClassementDtoHeader Liste { get; set; }
    }

    public class ClassementDtoHeader
    {


        [DataMember(Name = "histo")]
        [JsonProperty("histo")]
        public List<ClassementDto> Classements { get; set; }

    }
    public class ClassementDto
    {
        [DataMember(Name = "saison")]
        [JsonProperty("saison")]
        public string Saison { get; set; }
        [DataMember(Name = "phase")]
        [JsonProperty("phase")]
        public int Phase { get; set; }
        [DataMember(Name = "point")]
        [JsonProperty("point")]
        public double Point { get; set; }
    }

}
