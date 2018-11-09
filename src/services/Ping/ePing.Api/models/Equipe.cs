using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;

namespace ePing.Api.models
{
    [DebuggerDisplay("{DebuggerDsiplay}")]
    public class Equipe : Trackable
    {
        [Key]
        public string Libelle { get; set; }

        public string Division { get; set; }

        public string LienDivision { get; set; }

        public int IdEpreuve { get; set; }

        public string Epreuve { get; set; }
        //  "cx_poule=1290807&D1=3799&organisme_pere=1002"

        public int IdPoule { get; set; }

        public int IdDivision { get; set; }

        public string IdOrganismePere { get; set; }

        [NotMapped]
        public Organisme Organisme { get;  set; }

        public Club Club { get; set; }

        public string Nom => Libelle.Split("-")[0].Trim();



        public int? Classement
        {
            get
            {
                var result = Classements?.IndexOf(Classements?.Where(c => c.LibelleEquipe== this.Nom).FirstOrDefault());
                return result!=null?result+1: 0;
            }
        }


        public string Points
        {
            get
            {

                return Classements?.Where(c => c.LibelleEquipe == this.Nom).FirstOrDefault()?.PointRencontre ?? string.Empty;
            }
        }

        public string NombreJoue
        {
            get
            {
                return Classements?.Where(c => c.LibelleEquipe == this.Nom).FirstOrDefault()?.NombreJoue ?? string.Empty;
            }
        }

        //[NotMapped]
        public List<ResultatRencontre> Resultats { get; set; } = new List<ResultatRencontre>();

        //[NotMapped]
        public List<ClassementEquipe> Classements { get; set; } = new List<ClassementEquipe>();
        public int NombreMatchGagne { get;  set; }
        public int NombreMatchNul { get;  set; }
        public int NombreMatchPerdu { get;  set; }
        [NotMapped]
        public IEnumerable<Tour> Tours { get; set; } = new List<Tour>();
        public string Type { get; internal set; }

        private string DebuggerDisplay => $"Libelle:{this.Libelle} - Division:{this.Division} ";
    }
}
