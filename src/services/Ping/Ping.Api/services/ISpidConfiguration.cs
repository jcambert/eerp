using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ping.Api.services
{
    public interface ISpidConfiguration
    {
        string this[string key] { get; }

        string Serie { get; }

        string LicenceJoueur { get; }

        string Club { get; }
        string ClubDetail { get; }
        string ApplicationId { get; }
        string ApiEndpoint { get; }
        string ApiName { get; }
        string Departement { get; }
        string ClubFFTT { get; }
        string Organisme { get; }
        string Epreuve { get;  }
        string DivisionEpreuve { get; }
        string RencontreResultat { get; }
        string RencontreResultatDetail { get; }
        string ClubEquipe { get; }
        string ClassementDivision { get; }
        string ListeJoueur { get; }
        string Joueur { get; }
        string JoueurSpid { get; }
        string JoueurPartieSpid { get; }
        string JoueurPartieMySql { get; }
        string HistoriqueClassement { get; }

    }

    public class SpidConfiguration : ISpidConfiguration
    {
        private readonly IConfiguration _configuration;

        public SpidConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string this[string key]=>_configuration[key];
        
        public string LicenceJoueur => "LicenceJoueur";

        public string Club =>"Club";

        public string ClubFFTT => "ClubFFTT";

        public string ClubDetail => "ClubDetail";

        public string ApiName => this["spid:name"];

        public string Serie => this["spid:serie"];

        public string ApplicationId => this["spid:appid"];

        public string ApiEndpoint => this["spid:api:endpoint"];

        public string Departement => "Departement";

        public string Organisme=> "Organisme";

        public string Epreuve => "Epreuve";

        public string DivisionEpreuve => "DivisionEpreuve";

        public string RencontreResultat => "RencontreResultat";

        public string RencontreResultatDetail => "RencontreResultatDetail";

        public string ClubEquipe => "ClubEquipe";

        public string ClassementDivision => "ClassementDivision";

        public string ListeJoueur => "ListeJoueur";

        public string Joueur => "Joueur";

        public string JoueurSpid => "JoueurSpid";

        public string JoueurPartieSpid => "JoueurPartieSpid";

        public string JoueurPartieMySql => "JoueurPartieMySql";

        public string HistoriqueClassement => "HistoriqueClassement";
    }
}
