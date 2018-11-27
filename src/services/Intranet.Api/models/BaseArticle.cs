using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.models
{
    public abstract class BaseArticle:IdTrackable
    {
        [Required]
        [MinLength(3),MaxLength(25)]
        public string Reference { get; set; }
        [Required]
        [MinLength(1),MaxLength(6)]
        public string Indice { get; set; }
    }
}
