using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Auth.Api.dto
{
    [DataContract]
    public class ListesJoueurHeader
    {
        [DataMember(Name = "liste")]
        public JoueursDtoHeader Liste { get; set; }
    }

    public class JoueursDtoHeader
    {
   
        [DataMember(Name = "joueur")]
        [JsonProperty("joueur")]
        public List<JoueurDto> Joueurs { get; set; }

    }

    [DataContract]
    public class ListeJoueurHeader
    {
        [DataMember(Name ="liste")]
        public JoueurDtoHeader Liste { get; set; }
    }

    public class JoueurDtoHeader
    {
        [DataMember(Name ="joueur")]
        [JsonProperty("joueur")]
        public JoueurDto Joueur { get; set; }

        [DataMember(Name = "licence")]
        [JsonProperty("licence")]
        public JoueurDto Licence { get; set; }


    }
    public class JoueurDto
    {
        [DataMember(Name = "licence")]
        [JsonProperty("licence")]
        public string Licence { get; set; }
        [DataMember(Name = "nom")]
        [JsonProperty("nom")]
        public string Nom { get; set; }
        [DataMember(Name = "prenom")]
        [JsonProperty("prenom")]
        public string Prenom { get; set; }
        [DataMember(Name = "club")]
        [JsonProperty("club")]
        public string Club { get; set; }
        [DataMember(Name = "sexe")]
        [JsonProperty("sexe")]
        public string Sexe { get; set; }
        [DataMember(Name = "echelon")]
        [JsonProperty("echelon")]
        public string Echelon { get; set; }
        [DataMember(Name = "place")]
        [JsonProperty("place")]
        public string Place { get; set; }
        [DataMember(Name = "point")]
        [JsonProperty("point")]
        public string Point { get; set; }
        [DataMember(Name = "numclub")]
        [JsonProperty("numclub")]
        public object NumeroClub { get; set; }
        [DataMember(Name = "nclub")]
        [JsonProperty("nclub")]
        public object NClub { get; set; }
        [DataMember(Name = "nomclub")]
        [JsonProperty("nomclub")]
        public object NomClub { get; set; }
        [DataMember(Name = "type")]
        [JsonProperty("type")]
        public object Type { get; set; }
        [DataMember(Name = "certif")]
        [JsonProperty("certif")]
        public object Certficat { get;  set; }
        [DataMember(Name = "validation")]
        [JsonProperty("validation")]
        public object Validation { get;  set; }
        [DataMember(Name = "cat")]
        [JsonProperty("cat")]
        public object Categorie { get;  set; }
    }
}
