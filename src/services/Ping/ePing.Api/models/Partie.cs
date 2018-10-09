using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    public class Partie:Trackable
    {

        public string Date { get; set; }

        public string Nom { get; set; }

        public double Classement { get; set; }

        public string Epreuve { get; set; }

        public string Victoire { get; set; }

        public string Forfait { get; set; }

        public string IdPartie { get; set; }

        public double PointsGagnesPerdus { get; set; }
    }
}
