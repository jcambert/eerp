using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing
{
    public class AppSettings
    {
    }
    public class ApiSettings
    {
        public string EndPoint {get;set;}

        public string Club { get; set; }

        public string JoueursDuClub { get; set; }

        public string ReloadJoueursDuClub { get; set; }

        public string PartiesDuJoueur { get; set; }

        public string HistoriquesDuJoueur { get; set; }
    }
}
