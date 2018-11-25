using AutoMapper;
using ePing.Api.dto;
using ePing.Api.models;
using System;
using System.Text.RegularExpressions;

namespace ePing.Api
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<string, int>().ConstructUsing(s =>
            {
                if (s == null) return 0;
                int result;
                if (int.TryParse("0" + s, out result)) return result;
                return 0;
            });
            CreateMap<ClubDto, Club>().ReverseMap();
            CreateMap<JoueurDto, Joueur>().ConstructUsing(j=> {
                int nationnal, classement;
                TryConvertClassement(j.Classement, out nationnal, out classement);
                return new Joueur() {
                    AncienClassementGlobal = j.AncienClassementGlobal,
                    AncienPoint=j.AncienPoint,
                    Categorie=j.Categorie,
                    Classement=classement,
                    ClassementGlobal=j.ClassementGlobal,
                    ClassementNational=j.ClassementNational,
                    Club=j.Club,
                    Echelon=j.Echelon,
                    Licence=j.Licence,
                    Nationalite=j.Nationalite,
                    Nom=j.Nom,
                    Place=j.Place,
                    Point=j.Point,
                    PointDebut=j.PointDebut,
                    NumeroClub=j.NumeroClub,
                    PointOfficiel=j.PointOfficiel,
                    Prenom=j.Prenom,
                    PropositionClassement=j.PropositionClassement,
                    RangDepartemental=j.RangDepartemental,
                    RangRegional=j.RangRegional,
                    Sexe=j.Sexe

                };
            });
            CreateMap<PartieDto, Partie>().ConstructUsing(p =>
            {
                DateTime d;
                if (!DateTime.TryParse(p.Date, out d))
                    d = DateTime.Now;
                int nationnal, classement;
                TryConvertClassement(p.Classement, out nationnal, out classement);
                return new Partie()
                {
                    Classement = classement,
                    RangNationnal=nationnal,
                    Date = d,
                    Epreuve = p.Epreuve,
                    Forfait = p.Forfait,
                    IdPartie = p.IdPartie,
                    Nom = p.Nom,
                    Victoire = p.Victoire

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
            CreateMap<EquipeDto, Equipe>();
            CreateMap<OrganismeDto, Organisme>();
            CreateMap<ResultatRencontreDto, ResultatRencontre>().ConstructUsing(x =>
            {
               // return new ResultatRencontre();
                DateTime dt;
                try
                {


                    if (!DateTime.TryParse(x.DatePrevue, out dt))
                        dt = DateTime.Now;
                    int scoreA = 0, scoreB = 0;
                    /*if (x.ScoreA != null && !int.TryParse("0"+x.ScoreA, out scoreA))
                        scoreA = 0;
                    if (x.ScoreB != null && !int.TryParse("0"+x.ScoreB, out scoreB))
                        scoreB = 0;*/
                    var result = new ResultatRencontre()
                    {
                        DatePrevue = dt,
                        DateReelle = x.DateReelle,
                        EquipeA = x.EquipeA,
                        EquipeB = x.EquipeB,
                        Libelle = x.Libelle,
                        LienRencontre = x.LienRencontre,
                        RealScoreA =  x.ScoreA ?? "",
                        RealScoreB =  x.ScoreB ?? "",
                        ScoreA = scoreA,
                        ScoreB =  scoreB
                    };
                    return result;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    throw ex;
                }
            }).AfterMap((dto, model) =>
            {

            });
            CreateMap<ClassementEquipeDto, ClassementEquipe>().ConvertUsing(x =>
            {
                return new ClassementEquipe()
                {
                    Classement = x.Classement,
                    LibelleEquipe = x.Equipe,
                    NombreJoue = x.NombreJoue,
                    Numero = x.Numero,
                    PointRencontre = x.PointRencontre,
                    LibellePoule = x.Poule

                };
            });

        }

        private void TryConvertClassement(string cl,out int nationnal,out int classement)
        {
            nationnal = 0; classement = 0;
            if (cl == null) return;
            var pattern = @"\d+";
            var matches=Regex.Matches(cl, pattern);
            
            if (matches.Count == 0) return ;
            if (matches.Count == 1)
                classement = int.Parse( matches[0].Value);
            if (matches.Count == 2)
            {
                nationnal = int.Parse(matches[0].Value);
                classement = int.Parse( matches[1].Value);
            }
        }
    }
}
