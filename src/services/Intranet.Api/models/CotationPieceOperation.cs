using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.models
{
    public class CotationPieceOperation : IdTrackable
    {
        [Required]
        [MinLength(3),MaxLength(25)]
        public string Designation { get; set; }

        public string Aide { get; set; } = "";

        [Range(0.0,double.MaxValue)]
        public double Nombre { get; set; }
        [Range(0, int.MaxValue)]
        public int TauxHorrairePreparation { get; set; }
        [Range(0.0, double.MaxValue)]
        public double TempsPreparation { get; set; }
        [Range(0, int.MaxValue)]
        public int TauxHorraireOperation { get; set; }
        [Range(0.0, double.MaxValue)]
        public double TempsOperation { get; set; }

        #region Navigation / Relationship
        public CotationPiece Piece { get;  set; }
        #endregion
    }
}
