﻿using EntityFrameworkCore.Rx;
using EntityFrameworkCore.Triggers;
using ePing.Api.models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ePing.Api.dbcontext
{
    public class OldPingDbContext : DbContextWithTriggers
    {
        private IObservable<IAfterEntry<Joueur, OldPingDbContext>> joueurSpidObserveInserted;
      //  private IObservable<IAfterEntry<Organisme, PingDbContext>> organismeObserveInserted;

        public OldPingDbContext()
        {
            Initialize();
        }
        public OldPingDbContext(DbContextOptions options) : base(options)
        {
            Initialize();
        }

        private void Initialize()
        {
           // Random rnd = new Random();
            joueurSpidObserveInserted = DbObservable<OldPingDbContext>.FromInserted<Joueur>();
            joueurSpidObserveInserted.Subscribe(entry =>
            {
                var joueur = entry.Entity;
                var extra = new JoueurExtra() { Licence = joueur.Licence };
                joueur.Extra = extra;
            });

           /* organismeObserveInserted = DbObservable<PingDbContext>.FromInserted<Organisme>();
            organismeObserveInserted.Subscribe(entry =>
            {
                entry.Entity.Id = rnd.Next().ToString();
            });*/
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Joueur> Joueurs { get; set; }
        public DbSet<JoueurExtra> JoueursExtra { get; set; }
        public DbSet<Cache> Cache { get; set; }
        public DbSet<ClassementEquipe> ClassementsEquipes { get; set; }
        public DbSet<ResultatRencontre> ResultatsRencontres { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        //public DbSet<JourneeHistoriques> JourneeHistoriques { get; set; }
        //public DbSet<Historique> Historiques { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Club>().HasMany<Joueur>().WithOne(j => j.ClubRelation).OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Joueur>()
            .HasOne<JoueurExtra>(a => a.Extra)
            .WithOne(b => b.Joueur)
            
            .HasForeignKey<JoueurExtra>(e => e.LicenceOfJoueurSpid)
            .OnDelete(DeleteBehavior.Cascade)
            ;



        }
        public override void Dispose()
        {
            base.Dispose();
            //joueurSpidObserveInserted.Dispose();
        }
        public DbSet<ePing.Api.models.Organisme> Organismes { get; set; }
    }
}
