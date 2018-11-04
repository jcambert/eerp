using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    public class Tour
    {
        public string Date { get; set; }

        public List<ResultatRencontre> Resultats { get; set; }
    }
}
