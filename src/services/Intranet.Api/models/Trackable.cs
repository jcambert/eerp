using EntityFrameworkCore.Triggers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.models
{
    public abstract class Trackable
    {
        public DateTime InsertedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public string InsertedBy { get; set; } = "";
        public string UpdatedBy { get; set; } = "";


        static Trackable()
        {
            Triggers<Trackable>.Inserting += e => e.Entity.InsertedAt = e.Entity.UpdatedAt = DateTime.UtcNow;
            Triggers<Trackable>.Updating += e => e.Entity.UpdatedAt = DateTime.UtcNow;
        }
    }

    public abstract class IdTrackable : Trackable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
