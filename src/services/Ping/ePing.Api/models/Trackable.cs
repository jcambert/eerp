using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
namespace ePing.Api.models
{
    public abstract class Trackable
    {
        public DateTime Inserted { get; protected set; }
        public DateTime Updated { get; protected set; }

        static Trackable()
        {
            Triggers<Trackable>.Inserting += e => e.Entity.Inserted = e.Entity.Updated = DateTime.UtcNow;
            Triggers<Trackable>.Updating += e => e.Entity.Updated = DateTime.UtcNow;
        }
    }
}
