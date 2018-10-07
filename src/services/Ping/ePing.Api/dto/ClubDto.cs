using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ePing.Api.dto
{

    [DataContract()]
    public class ListeClubHeader
    {
        [DataMember(Name = "liste")]
        public ClubDtoHeader Liste { get; set; }

    }
    
    
    public class ClubDtoHeader
    {
        [DataMember(Name = "club")]
        public ClubDto Club { get; set; }

    }

    public class ClubDto
    {
        [DataMember(Name = "idclub")]
        [JsonProperty("idclub")]
        public string idClub { get; set; }
        [DataMember(Name = "numero")]
        [JsonProperty("numero")]
        public string Numero { get; set; }
        [DataMember(Name = "nom")]
        [JsonProperty("nom")]
        public string Nom { get; set; }
        [DataMember(Name = "nomsalle")]
        [JsonProperty("nomsalle")]
        public string NomSalle { get; set; }
        [DataMember(Name = "adressesalle1")]
        [JsonProperty("adressesalle1")]
        public string AdresseSalle1 { get; set; }
        [DataMember(Name = "adressesalle2")]
        [JsonProperty("adressesalle2")]
        public string  AdresseSalle2 { get; set; }
        [DataMember(Name = "adressesalle3")]
        [JsonProperty("adressesalle3")]
        public string AdresseSalle3 { get; set; }
        [DataMember(Name = "codepsalle")]
        [JsonProperty("codepsalle")]
        public string CodePostalSalle { get; set; }
        [DataMember(Name = "villesalle")]
        [JsonProperty("villesalle")]
        public string VilleSalle { get; set; }
        [DataMember(Name = "web")]
        [JsonProperty("web")]
        public string SiteWeb { get; set; }
        [DataMember(Name = "nomcor")]
        [JsonProperty("nomcor")]
        public string  NomCorrespondant { get; set; }
        [DataMember(Name = "prenomcor")]
        [JsonProperty("prenomcor")]
        public string PrenomCorrespondant { get; set; }
        [DataMember(Name = "mailcor")]
        [JsonProperty("mailcor")]
        public string MailCorrespondant { get; set; }
        [DataMember(Name = "telcor")]
        [JsonProperty("telcor")]
        public string TelephoneCorrespondant { get; set; }
        [DataMember(Name = "longitude")]
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
        [DataMember(Name = "latitude")]
        [JsonProperty("latitude")]
        public string Latitude{ get; set; }
        [DataMember(Name = "datevalidation")]
        [JsonProperty("datevalidation")]
        public string DateValidation { get; set; }

    }
}
