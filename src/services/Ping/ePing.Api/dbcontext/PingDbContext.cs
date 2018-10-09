﻿using EntityFrameworkCore.Triggers;
using ePing.Api.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.dbcontext
{
    public class PingDbContext: DbContextWithTriggers
    {
        public PingDbContext()
        {

        }
        public PingDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Club> Clubs{ get; set; }
        public DbSet<ePing.Api.models.Joueur> Joueur { get; set; }

    }
}