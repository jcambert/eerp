using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    public class ClassementEquipe:Trackable
    {
        public string Poule { get; set; }

        public string Classement { get; set; }

        public string Equipe { get; set; }

        public string NombreJoue { get; set; }

        public string PointRencontre { get; set; }

        public string Numero { get; set; }
    }
}
