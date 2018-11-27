using EntityFrameworkCore.Triggers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Api.models
{
    public class CotationPiece:IdTrackable
    {
        [Required]
        [MinLength(3),MaxLength(25)]
        public string Reference { get; set; }

        public string Designation { get; set; } = "";

        [Range(0,int.MaxValue)]
        public int Longueur { get; set; }
        [Range(0, int.MaxValue)]
        public int Largeur { get; set; }
        [Range(0.0, double.MaxValue)]
        public double Epaisseur { get; set; }
        [Range(0, int.MaxValue)]
        public int SqueletteX { get; set; }
        [Range(0, int.MaxValue)]
        public int SqueletteY { get; set; }
        [Range(0, int.MaxValue)]
        public int Pince { get; set; }
        [Range(0, int.MaxValue)]
        public int LongueurMeo { get; set; }
        [Range(0, int.MaxValue)]
        public int LargeurMeo { get; set; }
        [Range(0, int.MaxValue)]
        public int QuantiteMeo { get; set; }

        public string InformationMeo { get; set; } = "";

        [Required]
        [MinLength(3), MaxLength(25)]
        public string TypeMatiere { get; set; }
        [Required]
        [Range(0.0, double.MaxValue)]
        public double DensiteMatiere { get; set; }

        [Required]
        [MinLength(3), MaxLength(25)]
        public string Matiere { get; set; }
        [Required]
        [Range(0.0,double.MaxValue)]
        public double PrixMatiere { get; set; }

        #region Navigation / Relationship
        public Cotation Cotation { get; set; }

        public List<CotationPieceFormat> Formats { get; set; } = new List<CotationPieceFormat>();

        public List<CotationPieceComposant> Composants { get; set; } = new List<CotationPieceComposant>();

        public List<CotationPieceOperation> Operations { get;  set; } = new List<CotationPieceOperation>();
        #endregion

        static CotationPiece()
        {
            //Triggers<CotationPiece>.Inserting += e => e.Entity.InsertedAt = e.Entity.UpdatedAt = DateTime.UtcNow;
            //Triggers<CotationPiece>.Updating += e => e.Entity.UpdatedAt = DateTime.UtcNow;
        }
        
    }
}