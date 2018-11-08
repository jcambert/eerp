using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    public class ResultatRencontre:Trackable
    {
        [Key]
        public int Id { get; set; }

        public string Libelle { get; set; }

        public string EquipeA { get; set; }

        public string EquipeB { get; set; }

        public int? ScoreA { get; set; }

        public int? ScoreB { get; set; }

        public string LienRencontre { get; set; }

        public DateTime DatePrevue { get; set; }

        public string DateReelle { get; set; }
        public int Poule { get;  set; }
        public int Division { get;  set; }


        public string EquipeLibelle { get;  set; }
    }
}
