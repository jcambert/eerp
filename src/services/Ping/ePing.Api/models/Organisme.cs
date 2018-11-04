using System.ComponentModel.DataAnnotations;

namespace ePing.Api.models
{
    public class Organisme : Trackable
    {
        [Key]
        public string Id { get; set; }

        public string Identifiant { get; set; }

        public string Libelle { get; set; }

        public string Code { get; set; }

        public string IdPere { get; set; }
    }
}
