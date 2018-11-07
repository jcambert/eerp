using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ePing.Api.models
{
    public class Organisme : Trackable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id {
            get;
            set; }

        public string Identifiant { get; set; }

        public string Libelle { get; set; }

        public string Code { get; set; }

        public string IdPere { get; set; }

        public List<Equipe> Equipes { get; set; } = new List<Equipe>();
    }
}
