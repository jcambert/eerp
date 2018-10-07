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
        [Key]
        public string Licence { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        [IgnoreMap]
        public Club Club { get; set; }

        public string Sexe { get; set; }

        public string  Echelon { get; set; }

        public string Place { get; set; }

        public int Point { get; set; }

        public int Classe =>(Point/100);

    }
}
