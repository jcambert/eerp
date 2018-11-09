using AutoMapper;
using ePing.Api.dto;
using ePing.Api.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<ClubDto, Club>().ReverseMap();
            CreateMap<JoueurDto, Joueur>().ReverseMap();
            CreateMap<PartieDto, Partie>().ConstructUsing(p =>
            {
                DateTime d;
                if (!DateTime.TryParse(p.Date, out d))
                    d = DateTime.Now;
                return new Partie()
                {
                    Classement = p.Classement,
                    Date = d,
                    Epreuve=p.Epreuve,
                    Forfait=p.Forfait,
                    IdPartie=p.IdPartie,
                    Nom=p.Nom,
                    Victoire=p.Victoire
                    
                };
            });
            CreateMap<PartieHistoDto, Historique>();
            CreateMap<ClassementDto, Classement>();
            CreateMap<Journee, HistoriquePointDto>().ConvertUsing(journee =>
            {
                return new HistoriquePointDto() { Date = journee.Date, PointsGagnesPerdus = journee.PointsGagnesPerdus };
            });
            CreateMap<Journee, HistoriqueVictoireDto>().ConstructUsing(journee =>
            {
                return new HistoriqueVictoireDto() { Date = journee.Date, Victoire = journee.NombreVictoire };
            });

            CreateMap<Journee, HistoriqueDefaiteDto>().ConstructUsing(journee =>
            {
                return new HistoriqueDefaiteDto() { Date = journee.Date, Defaite = journee.NombreDefaite };
            });
            CreateMap<EquipeDto, Equipe>().ReverseMap();
            CreateMap<OrganismeDto, Organisme>();
            CreateMap<ResultatRencontreDto, ResultatRencontre>().ConstructUsing(x=>
            {
                DateTime dt;
                if (!DateTime.TryParse(x.DatePrevue, out dt))
                    dt = DateTime.Now;
                int scoreA, scoreB;
                if(!)
                return new ResultatRencontre() {
                    DatePrevue=dt,
                    DateReelle=x.DateReelle,
                    EquipeA=x.EquipeA,
                    EquipeB=x.EquipeB,
                    Libelle=x.Libelle,
                    LienRencontre=x.LienRencontre,
                    RealScoreA=x.ScoreA,
                    RealScoreB=x.scoreB,
                    ScoreA=x.ScoreA ?? 0,
                    ScoreB=x.ScoreB ?? 0
                };
            });
            CreateMap<ClassementEquipeDto, ClassementEquipe>().ConvertUsing(x=>
            {
                return new ClassementEquipe()
                {
                    Classement = x.Classement,
                    LibelleEquipe=x.Equipe,
                    NombreJoue=x.NombreJoue,
                    Numero=x.Numero,
                    PointRencontre=x.PointRencontre,
                    LibellePoule= x.Poule

                };
            });

        }
    }
}
