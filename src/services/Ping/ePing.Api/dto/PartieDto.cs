using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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

        [DataMember(Name = "partie")]
        [JsonProperty("partie")]
        public List<PartieHistoDto> Historiques { get; set; }

    }
    public class PartieDto
    {
        [DataMember(Name = "date")]
        [JsonProperty("date")]
        public string Date { get; set; }
        [DataMember(Name = "nom")]
        [JsonProperty("nom")]
        public string Nom { get; set; }
        [DataMember(Name = "classement")]
        [JsonProperty("classement")]
        public string Classement { get; set; }
        [DataMember(Name = "epreuve")]
        [JsonProperty("epreuve")]
        public string Epreuve { get; set; }
        [DataMember(Name = "victoire")]
        [JsonProperty("victoire")]
        public string Victoire { get; set; }
        [DataMember(Name = "forfait")]
        [JsonProperty("forfait")]
        public string Forfait { get; set; }
        [DataMember(Name = "idpartie")]
        [JsonProperty("idpartie")]
        public string IdPartie { get; set; }
    }

    public class PartieHistoDto
    {
        [DataMember(Name = "licence")]
        [JsonProperty("licence")]
        public string Licence { get; set; }
        [DataMember(Name = "advlic")]
        [JsonProperty("advlic")]
        public string LicenceAdversaire { get; set; }
        [DataMember(Name = "vd")]
        [JsonProperty("vd")]
        public string Victoire { get; set; }
        [DataMember(Name = "numjourn")]
        [JsonProperty("numjourn")]
        public string Journee { get; set; }
        [DataMember(Name = "codechamp")]
        [JsonProperty("codechamp")]
        public string CodeChampionnat { get; set; }
        [DataMember(Name = "date")]
        [JsonProperty("date")]
        public string Date { get; set; }
        [DataMember(Name = "advsexe")]
        [JsonProperty("advsexe")]
        public string SexeAdversaire { get; set; }
        [DataMember(Name = "advnompre")]
        [JsonProperty("advnompre")]
        public string NomPrenomAdversaire { get; set; }
        [DataMember(Name = "pointres")]
        [JsonProperty("pointres")]
        public string PointsGagnesPerdus { get; set; }
        [DataMember(Name = "coefchamp")]
        [JsonProperty("coefchamp")]
        public string CoefficientEpreuve { get; set; }
        [DataMember(Name = "advclaof")]
        [JsonProperty("advclaof")]
        public string ClassementAdversaire { get; set; }
        [DataMember(Name = "idpartie")]
        [JsonProperty("idpartie")]
        public string IdPartie { get; set; }
    }
}
