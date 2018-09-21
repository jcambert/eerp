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
    }
}
