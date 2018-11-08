using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    public class JoueurExtra:Trackable
    {
        [Key]
        public string Licence { get; set; }

        public string Telephone { get; set; } = "";

        public string Email { get; set; } = "";

        [IgnoreMap]
        public Joueur Joueur { get; set; }
        [IgnoreMap]
        public string LicenceOfJoueurSpid { get; set; }
    }
}
