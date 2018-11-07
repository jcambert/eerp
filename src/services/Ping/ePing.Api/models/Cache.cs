using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    public class Cache:Trackable
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Equipe Equipe { get; set; }

        public DateTime? ClassementLimit { get; set; }

        public DateTime ResultatRencontreLimit { get; set; }
    }
}
