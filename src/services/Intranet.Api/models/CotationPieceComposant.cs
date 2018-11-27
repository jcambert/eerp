using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.models
{
    public class CotationPieceComposant:IdTrackable
    {
        [Required]
        [MinLength(3),MaxLength(25)]
        public string Designation { get; set; }

        public string Information { get; set; } = "";

        [Range(0.0, double.MaxValue)]
        public double Quantite { get; set; }

        [Range(0.0, double.MaxValue)]
        public double PrixUnitaire { get; set; }

        #region Navigation / Relationship
        public CotationPiece Piece { get;  set; }
        #endregion
    }
}
