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
        [DataMember(Name = "liste")]
        [JsonProperty("liste")]
        public JoueurDtoHeader Liste { get; set; }
    }

    public class JoueurDtoHeader
    {
        [DataMember(Name = "joueur")]
        [JsonProperty("joueur")]
        public JoueurDto Joueur { get; set; }

    }

    [DataContract]
    public class ListeJoueursHeader
    {
        [DataMember(Name ="liste")]
        [JsonProperty("liste")]
        public JoueursDtoHeader Liste { get; set; }
    }

    public class JoueursDtoHeader
    {
        [DataMember(Name ="joueur")]
        [JsonProperty("joueur")]
        public List<JoueurDto> Joueurs { get; set; }

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
        public string Club {get;set; }
        [DataMember(Name = "nclub")]
        [JsonProperty("nclub")]
        public string NumeroClub { get; set; }
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
        public double Point { get; set; }

        [DataMember(Name = "natio")]
        [JsonProperty("natio")]
        public string Nationalite { get; set; }
        [DataMember(Name = "clglob")]
        [JsonProperty("clglob")]
        public int ClassementGlobal { get; set; }
        [DataMember(Name = "aclglob")]
        [JsonProperty("aclglob")]
        public int AncienClassementGlobal { get; set; }
        [DataMember(Name = "apoint")]
        [JsonProperty("apoint")]
        public double AncienPoint { get; set; }
        [DataMember(Name = "clast")]
        [JsonProperty("clast")]
        public string Classement { get; set; }
        [DataMember(Name = "clnat")]
        [JsonProperty("clnat")]
        public int ClassementNational { get; set; }
        [DataMember(Name = "categ")]
        [JsonProperty("categ")]
        public string Categorie { get; set; }
        [DataMember(Name = "rangreg")]
        [JsonProperty("rangreg")]
        public int RangRegional { get; set; }
        [DataMember(Name = "rangdep")]
        [JsonProperty("rangdep")]
        public int RangDepartemental { get; set; }
        [DataMember(Name = "valcla")]
        [JsonProperty("valcla")]
        public int PointOfficiel { get; set; }
        [DataMember(Name = "clpro")]
        [JsonProperty("clpro")]
        public string PropositionClassement { get; set; }
        [DataMember(Name = "valinit")]
        [JsonProperty("valinit")]
        public double PointDebut { get; set; }


    }
}
