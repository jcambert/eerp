using AutoMapper;
using EntityFrameworkCore.Rx;
using EntityFrameworkCore.Triggers;
using Intranet.Api.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.dbcontext
{
    public class IntranetDbContext: DbContextWithTriggers
    {
        private readonly ApiSettings _settings;
        private readonly IMapper _mapper;
        private IObservable<IBeforeEntry<CotationPiece, IntranetDbContext>> _cotationPieceObserveInserted;

        public IntranetDbContext()
        {

        }
        public IntranetDbContext(DbContextOptions options,IOptions<ApiSettings> settings,IMapper mapper) : base(options)
        {
            _settings = settings.Value;
            _mapper = mapper;
            Initialize();
        }

        private void Initialize()
        {

            _cotationPieceObserveInserted = DbObservable<IntranetDbContext>.FromInserting<CotationPiece>();
            _cotationPieceObserveInserted.Subscribe(entry =>
            {
                var _params = this.Parametres.Local.Where(p => p.CodePrimaire == _settings.CodePrimaire.Format && p.CodeSecondaire > 0).ToList();
                foreach (var _param in _params)
                {

                    var cotpiecform = _mapper.Map<CotationPieceFormat>(_param);
                    if (cotpiecform == null) continue;
                    cotpiecform.Piece = entry.Entity;
                    entry.Entity.Formats.Add(cotpiecform);

                    CotationsPiecesFormats.Add(cotpiecform);
                }
            });

        }

        public DbSet<Parametre> Parametres { get; set; }
        public DbSet<Cotation> Cotations { get; set; }
        public DbSet<CotationPiece> CotationsPieces { get; set; }
        public DbSet<CotationPieceFormat> CotationsPiecesFormats { get; set; }
        public DbSet<CotationPieceComposant> CotationsPiecesComposants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Parametre>().HasAlternateKey(c => new { c.CodePrimaire,c.CodeSecondaire}).HasName("IX_Parametre");
            modelBuilder.Entity<Cotation>().HasAlternateKey(c => new{ c.Reference, c.Indice ,c.Version}).HasName("IX_Cotation");


            modelBuilder.Entity<Cotation>().HasMany<CotationPiece>(c => c.Pieces).WithOne(c => c.Cotation);
            modelBuilder.Entity<CotationPiece>().HasMany<CotationPieceFormat>(c=>c.Formats).WithOne(c => c.Piece);
            modelBuilder.Entity<CotationPiece>().HasMany<CotationPieceComposant>(c=>c.Composants).WithOne(c => c.Piece);
            modelBuilder.Entity<CotationPiece>().HasMany<CotationPieceOperation>(c=>c.Operations).WithOne(c => c.Piece);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            /*optionsBuilder.UseSqlite(options=>
            {
                 

            });*/
        }


    }
}
