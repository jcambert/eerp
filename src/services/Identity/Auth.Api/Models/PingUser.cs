using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Api.Models
{
    public class PingUser:IdentityUser
    {
        public PingUser()
        {
           
        }
        //public string Licence { get; set; }
        public string Nom { get;  set; }
        public string Prenom { get;  set; }
        public string NumeroClub { get;  set; }
        public string NomClub { get;  set; }
        public string Sexe { get;  set; }
        public string Type { get;  set; }
        public string Certificat { get;  set; }
        public string Validation { get;  set; }
        public string Echelon { get;  set; }
        public string Place { get;  set; }
        public string Point { get;  set; }
        public string Categorie { get;  set; }
    }
}
