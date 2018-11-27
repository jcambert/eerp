using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.models
{




    public class Cotation: BaseArticle
    {
        [Required()]
        [Range(0, int.MaxValue)]
        public int Version { get; set; } = 0;

        public double CoefficientMatierePremiere { get; set; }

        public double CoefficientSousTraitance { get; set; }

        public string InformationClient { get; set; } = "";

        public string InformationTechnique { get; set; } = "";

        [Range(0.0, double.MaxValue)]
        public double TempsBureauEtudes { get; set; } = 0;

        [Range(0.0, int.MaxValue)]
        public int FraisAdministratifs { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int Outillage { get; set; } =0;

        /// <summary>
        /// Voir Parametre
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(50)]
        public string Difficulte { get; set; }

        /// <summary>
        /// 0=> a convenir
        /// 1=> 1 Semaine
        /// 2=> 2 Semaines
        /// etc..
        /// voir parametre
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(50)]
        public string Delai { get; set; }

        /// <summary>
        /// Voir Parametre
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(50)]
        public string Traitement { get; set; }

        #region Navigation / Relationship
        public List<CotationPiece> Pieces { get; set; } = new List<CotationPiece>();
        #endregion
    }
}
