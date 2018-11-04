﻿using EntityFrameworkCore.Rx;
using EntityFrameworkCore.Triggers;
using ePing.Api.models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ePing.Api.dbcontext
{
    public class PingDbContext : DbContextWithTriggers
    {
        private  IObservable<IAfterEntry<JoueurSpid, PingDbContext>> joueurSpidObserveInserted ;

        public PingDbContext()
        {
            Initialize();
        }
        public PingDbContext(DbContextOptions options) : base(options)
        {
            Initialize();
        }

        private void Initialize()
        {
            joueurSpidObserveInserted = DbObservable<PingDbContext>.FromInserted<JoueurSpid>();
            joueurSpidObserveInserted.Subscribe(entry =>
            {
                var joueur = entry.Entity;
                var extra = new Joueur() { Licence = joueur.Licence };
                joueur.Extra = extra;
            });
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<JoueurSpid> JoueurSpid { get; set; }
        public DbSet<Joueur> JoueursExtra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JoueurSpid>()
            .HasOne<Joueur>(a => a.Extra)
            .WithOne(b => b.JoueurSpid)
            .HasForeignKey<Joueur>(e => e.LicenceOfJoueurSpid);



        }
        public override void Dispose()
        {
            base.Dispose();
            //joueurSpidObserveInserted.Dispose();
        }
        public DbSet<ePing.Api.models.Organisme> Organisme { get; set; }
    }
}
