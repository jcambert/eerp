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

        public string Telephone { get; set; } = "";

        public string Email { get; set; } = "";

        [IgnoreMap]
        public JoueurSpid JoueurSpid { get; set; }
        [IgnoreMap]
        public string LicenceOfJoueurSpid { get; set; }
    }
}
