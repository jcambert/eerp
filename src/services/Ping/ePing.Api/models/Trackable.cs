using EntityFrameworkCore.Triggers;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace ePing.Api.models
{
    public abstract class Trackable
    {
        public DateTime Inserted { get;  set; }
        public DateTime Updated { get; set; }

        static Trackable()
        {
            Triggers<Trackable>.Inserting += e => e.Entity.Inserted = e.Entity.Updated = DateTime.UtcNow;
            Triggers<Trackable>.Updating += e => e.Entity.Updated = DateTime.UtcNow;
        }
    }

    public abstract class IdTrackable : Trackable
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
