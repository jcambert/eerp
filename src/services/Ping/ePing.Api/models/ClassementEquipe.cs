using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    public class ClassementEquipe:Trackable
    {
        [Key]
        public int Id { get; set; }

        public int Poule { get; set; }
        public string LibellePoule { get; set; }
        public string Classement { get; set; }
        [NotMapped]
        public Equipe Equipe { get; set; }

        public string LibelleEquipe { get; set; }

        public string NombreJoue { get; set; }

        public string PointRencontre { get; set; }

        public string Numero { get; set; }
        public int Division { get; internal set; }
    }
}
