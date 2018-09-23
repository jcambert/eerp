using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    public class Club:Trackable
    {
        
        public string idClub { get; set; }
        
        [Key]
        public string Numero { get; set; }

        public string Nom { get; set; }

        public string Validation { get; set; }

        public List<Joueur> Joueurs { get; set; }

    }
}
