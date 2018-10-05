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
        public string Licence { get; set; }
        [DataMember(Name = "nom")]
        public string Nom { get; set; }
        [DataMember(Name = "prenom")]
        public string Prenom { get; set; }
        [DataMember(Name = "club")]
        public string Club { get; set; }
        [DataMember(Name = "sexe")]
        public string Sexe { get; set; }
        [DataMember(Name = "echelon")]
        public string Echelon { get; set; }
        [DataMember(Name = "place")]
        public string Place { get; set; }
        [DataMember(Name = "point")]
        public string Point { get; set; }
        [DataMember(Name = "numclub")]
        public object NumeroClub { get; set; }
        [DataMember(Name = "nclub")]
        public object NClub { get; set; }
        [DataMember(Name = "nomclub")]
        public object NomClub { get; set; }
        [DataMember(Name = "type")]
        public object Type { get; set; }
        [DataMember(Name = "certif")]
        public object Certficat { get;  set; }
        [DataMember(Name = "validation")]
        public object Validation { get;  set; }
        [DataMember(Name = "cat")]
        public object Categorie { get;  set; }
    }
}
