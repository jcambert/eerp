using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EntityFrameworkCore.Rx;
using EntityFrameworkCore.Triggers;
using Intranet.Api.dbcontext;
using Intranet.Api.models;

namespace Intranet.Api.Seedings
{
    internal class CotationSeed : BaseSeed<Cotation>
    {
        private readonly IObservable<IBeforeEntry<CotationPiece, IntranetDbContext>> _cotationPieceObserveInserting;

        public CotationSeed(IntranetDbContext ctx, ApiSettings settings,IMapper mapper) : base(ctx, settings,mapper)
        {
            _cotationPieceObserveInserting = DbObservable<IntranetDbContext>.FromInserting<CotationPiece>();
            _cotationPieceObserveInserting.Subscribe(entry =>
            {
                var _params = Context.Parametres.Local.Where(p => p.CodePrimaire == Settings.CodePrimaire.Format && p.CodeSecondaire > 0).ToList();
                foreach (var _param in _params)
                {

                    var cotpiecform = Mapper.Map<CotationPieceFormat>(_param);
                    if (cotpiecform == null) continue;
                    cotpiecform.Piece = entry.Entity;
                }
            });
        }

        public override void Seed()
        {
            var piec0 = new CotationPiece() { Reference = "Ref1234", Designation = "Des1234", DensiteMatiere = 8, Epaisseur = 1, Largeur = 100, Longueur = 100, Matiere = "Xc10<3", TypeMatiere = "Acier", PrixMatiere = 0.78, Pince = 0, SqueletteX = 10, SqueletteY = 10 };
            var cot0 = new Cotation() { Reference = "Ref1234", Indice = "0", Version = 0, CoefficientMatierePremiere = 1.35, CoefficientSousTraitance = 1.25, Delai = "1 Semaine", Difficulte = "Facile", Traitement = "Brut" };
            cot0.Pieces.Add(piec0);
            Set.Add(cot0);
        }
    }
}
