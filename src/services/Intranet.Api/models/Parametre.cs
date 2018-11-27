using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.models
{
    public class Parametre:IdTrackable
    {
        /// <summary>
        /// Code Hierarchique
        /// Ex 100=>Matiere
        /// </summary>
        [Range(0,int.MaxValue)]
        public int CodePrimaire { get; set; }

        /// <summary>
        /// 100 + 1 =>Xc10<3
        /// 100 + 2 =>Xc10>3
        /// 200 + 1 =>304L
        /// 200 + 2 =>430
        /// </summary>
        [Range(0, int.MaxValue)]
        public int CodeSecondaire { get; set; }

        /// <summary>
        /// Delai
        /// difficulte
        /// Traitement
        /// Coef Mp
        /// Coef St
        /// matiere
        /// type matiere
        /// </summary>
        [Required]
        public string Type { get; set; }

        [Required]
        [MinLength(1),MaxLength(50)]
        public string Designation { get; set; }

        public int IntValue { get; set; } = 0;

        public double DoubleValue { get; set; } = 0.0;

        public string Remarque { get; set; } = "";

        public bool EstValide { get; set; } = true;
    }
}
