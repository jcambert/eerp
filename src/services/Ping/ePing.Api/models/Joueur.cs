using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
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

        public int PropositionClassement { get; set; }

        public double PointDebut { get; set; }

        public double ProgressionMensuelle =>  Point- (AncienPoint==0?500:AncienPoint);

        public double ProgressionAnnuelle => Point-(PointDebut==0?500: PointDebut);
        [IgnoreMap]
        public JoueurExtra Extra { get; set; } 

    }
}
