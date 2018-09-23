using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ePing.Api.dto
{

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
        public List<JoueurDto> Joueurs { get; set; }

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
    }
}
