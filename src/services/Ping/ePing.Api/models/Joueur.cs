using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Joueur:Trackable
    {
     
        private string _categorie;

        [Key]
        public string Licence { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }
        
        public string Club { get; set; }

        public string NumeroClub { get; set; }

        public string Sexe { get; set; }

        public string  Echelon { get; set; }

        public string Place { get; set; }

        public double Point { get; set; }

        //public int Classe =>(Point/100);
        public Club ClubRelation { get; set; }

        public string Nationalite { get; set; }

        public int ClassementGlobal { get; set; }

        public int AncienClassementGlobal { get; set; }

        public double AncienPoint { get; set; }

        public int Classement { get; set; }

        public int ClassementNational { get; set; }

        public string Categorie { get { return _categorie ?? "N"; } set { _categorie = value; } }

        public int RangRegional { get; set; }

        public int RangDepartemental { get; set; }

        public int PointOfficiel { get; set; }

        public string PropositionClassement { get; set; }

        public double PointDebut { get; set; }

        public double ProgressionMensuelle =>  Point- (AncienPoint==0?500:AncienPoint);

        public double ProgressionAnnuelle => Point-(PointDebut==0?500: PointDebut);
        [IgnoreMap]
        public JoueurExtra Extra { get; set; }
        public string Type { get;  set; }
        public string Certificat { get;  set; }
        public string Validation { get;  set; }
        public string Mutation { get;  set; }
        public string Arbitre { get;  set; }
        public string JugeArbitre { get;  set; }
        public string Tech { get;  set; }

        private string DebuggerDisplay => $"Licence:{this.Licence} - Nom:{this.Nom} Prenom:{this.Prenom}";

    }

    [DebuggerDisplay("{DebuggerDisplay}")]
    public class JoueurSpid : Trackable
    {


        [Key]
        public string IdLicence { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Licence { get; set; }

        public string NumClub { get; set; }

        public string NomClub { get; set; }

        public string Sexe { get; set; }

        public string Type { get; set; }

        public string Certificat { get; set; }

        public string Validation { get; set; }

        public string Echelon { get; set; }

        public string Place { get; set; }

        public string Points { get; set; }

        public string Categorie { get; set; }

        public string PointsMensuel { get; set; }
 
        public string APointsMensuel { get; set; }

        public string PointsInitial { get; set; }

        public string Mutation { get; set; }

        public string Nationalite { get; set; }

        public string Arbitre { get; set; }

        public string JugeArbitre { get; set; }

        public string Tech { get; set; }

        private string DebuggerDisplay => $"Licence:{this.Licence} - Nom:{this.Nom} Prenom:{this.Prenom}";

    }

    [DebuggerDisplay("{DebuggerDisplay}")]
    public class JoueurSearch:Trackable
    {

        [Key]
        public string Licence { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Club { get; set; }

        public string NomClub { get; set; }

        public string Sexe { get; set; }

        public string Echelon { get; set; }
 
        public string Place { get; set; }

        public string Point { get; set; }

        private string DebuggerDisplay => $"Licence:{this.Licence} - Nom:{this.Nom} Prenom:{this.Prenom}";
    }
}
