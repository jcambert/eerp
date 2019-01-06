using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ePing.Api.dto
{

    [DataContract]
    public class ListeJoueurSpidHeader
    {
        [DataMember(Name = "liste")]
        [JsonProperty("liste")]
        public JoueurSpidDtoHeader Liste { get; set; }
    }
    [DataContract]
    public class ListeJoueurHeader
    {
        [DataMember(Name = "liste")]
        [JsonProperty("liste")]
        public JoueurDtoHeader Liste { get; set; }
    }

    //FROM api/joueur/{licence}/spid
    public class JoueurSpidDtoHeader
    {
        [DataMember(Name = "licence")]
        [JsonProperty("licence")]
        public JoueurSpidDto Joueur { get; set; }

    }
    //FROM api/joueur/{licence}
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
    
    public class JoueurSpidDto
    {
        [DataMember(Name = "idlicence")]
        [JsonProperty("idlicence")]
        public string IdLicence { get; set; }
        [DataMember(Name = "nom")]
        [JsonProperty("nom")]
        public string Nom{ get; set; }
        [DataMember(Name = "prenom")]
        [JsonProperty("prenom")]
        public string Prenom { get; set; }
        [DataMember(Name = "licence")]
        [JsonProperty("licence")]
        public string Licence { get; set; }
        [DataMember(Name = "numclub")]
        [JsonProperty("numclub")]
        public string NumClub { get; set; }
        [DataMember(Name = "nomclub")]
        [JsonProperty("nomclub")]
        public string NomClub { get; set; }
        [DataMember(Name = "sexe")]
        [JsonProperty("sexe")]
        public string Sexe { get; set; }
        [DataMember(Name = "type")]
        [JsonProperty("type")]
        public string Type { get; set; }
        [DataMember(Name = "certif")]
        [JsonProperty("certif")]
        public string Certificat { get; set; }
        [DataMember(Name = "validation")]
        [JsonProperty("validation")]
        public string Validation { get; set; }
        [DataMember(Name = "echelon")]
        [JsonProperty("echelon")]
        public string Echelon { get; set; }
        [DataMember(Name = "place")]
        [JsonProperty("place")]
        public string Place { get; set; }
        [DataMember(Name = "point")]
        [JsonProperty("point")]
        public string Points { get; set; }
        [DataMember(Name = "cat")]
        [JsonProperty("cat")]
        public string Categorie { get; set; }
        [DataMember(Name = "pointm")]
        [JsonProperty("pointm")]
        public string PointsMensuel { get; set; }
        [DataMember(Name = "apointm")]
        [JsonProperty("apointm")]
        public string APointsMensuel { get; set; }
        [DataMember(Name = "initm")]
        [JsonProperty("initm")]
        public string PointsInitial { get; set; }
        [DataMember(Name = "mutation")]
        [JsonProperty("mutation")]
        public string Mutation { get; set; }
        [DataMember(Name = "natio")]
        [JsonProperty("natio")]
        public string Nationalite { get; set; }
        [DataMember(Name = "arb")]
        [JsonProperty("arb")]
        public string Arbitre { get; set; }
        [DataMember(Name = "ja")]
        [JsonProperty("ja")]
        public string JugeArbitre { get; set; }
        [DataMember(Name = "tech")]
        [JsonProperty("tech")]
        public string Tech { get; set; }

    }

    [DataContract]
    public class ListeJoueursSearchHeader
    {
        [DataMember(Name = "liste")]
        [JsonProperty("liste")]
        public JoueursSearchDtoHeader Liste { get; set; } = new JoueursSearchDtoHeader();
    }

    [DataContract]
    public class ListeJoueurSearchHeader
    {
        [DataMember(Name = "liste")]
        [JsonProperty("liste")]
        public JoueurSearchDtoHeader Liste { get; set; }
    }

    public class JoueursSearchDtoHeader
    {
        [DataMember(Name = "joueur")]
        [JsonProperty("joueur")]
        public List<JoueurSearchDto> Joueurs { get; set; } = new List<JoueurSearchDto>();

    }
    public class JoueurSearchDtoHeader
    {
        [DataMember(Name = "joueur")]
        [JsonProperty("joueur")]
        public JoueurSearchDto Joueur { get; set; }

    }
    public class JoueurSearchDto
    {
        [DataMember(Name = "licence")]
        [JsonProperty("licence")]
        public string  Licence { get; set; }
        [DataMember(Name = "nom")]
        [JsonProperty("nom")]
        public string Nom { get; set; }
        [DataMember(Name = "prenom")]
        [JsonProperty("prenom")]
        public string Prenom { get; set; }
        [DataMember(Name = "club")]
        [JsonProperty("club")]
        public string Club { get; set; }
        [DataMember(Name = "nclub")]
        [JsonProperty("nclub")]
        public string NomClub { get; set; }
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
    }
}
