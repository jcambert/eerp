using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ePing.Api.models
{
    [DebuggerDisplay("{DebuggerDsiplay}")]
    public class Club : Trackable
    {

        public string idClub { get; set; }

        [Key]
        public string Numero { get; set; }

        public string Nom { get; set; }


        public string NomSalle { get; set; }
        public string AdresseSalle1 { get; set; }
        public string AdresseSalle2 { get; set; }
        public string AdresseSalle3 { get; set; }
        public string CodePostalSalle { get; set; }
        public string VilleSalle { get; set; }
        public string SiteWeb { get; set; }
        public string NomCorrespondant { get; set; }
        public string PrenomCorrespondant { get; set; }
        public string MailCorrespondant { get; set; }
        public string TelephoneCorrespondant { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string DateValidation { get; set; }

        public List<Joueur> Joueurs { get; set; } = new List<Joueur>();

        public List<Equipe> Equipes { get; set; } = new List<Equipe>();

        private string DebuggerDisplay => $"Numero:{this.Numero} - Nom:{this.Nom} ";
    }
}
