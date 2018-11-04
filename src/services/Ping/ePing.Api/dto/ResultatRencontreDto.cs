using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ePing.Api.dto
{
    [DataContract()]
    public class ListeResultatsRencontresHeader
    {
        [DataMember(Name = "liste")]
        [JsonProperty("liste")]
        public ResultatsRencontresDtoHeader Liste { get; set; }

    }


    public class ResultatsRencontresDtoHeader
    {

        [DataMember(Name = "tour")]
        [JsonProperty("tour")]
        public List<ResultatRencontreDto> Resultats { get; set; }

    }
    public class ResultatRencontreDto
    {
        [DataMember(Name = "libelle")]
        [JsonProperty("libelle")]
        public string Libelle { get; set; }
        [DataMember(Name = "equa")]
        [JsonProperty("equa")]
        public string EquipeA { get; set; }
        [DataMember(Name = "equb")]
        [JsonProperty("equb")]
        public string EquipeB { get; set; }
        [DataMember(Name = "scorea")]
        [JsonProperty("scorea")]
        public int? ScoreA { get; set; }
        [DataMember(Name = "scoreb")]
        [JsonProperty("scoreb")]
        public int? ScoreB { get; set; }
        [DataMember(Name = "lien")]
        [JsonProperty("lien")]
        public string LienRencontre { get; set; }
        [DataMember(Name = "dateprevue")]
        [JsonProperty("dateprevue")]
        public string DatePrevue { get; set; }
        [DataMember(Name = "datereelle")]
        [JsonProperty("datereelle")]
        public string DateReelle { get; set; }
    }
}
