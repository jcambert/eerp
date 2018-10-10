using ePing.Api.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    public static class JourneeExtensions
    {
        public static void Add(this Journee journee,JoueurSpid joueur, Partie partie,PointService service)
        {
            var pts=service.CalculPointsGagnePerdu(joueur.PointOfficiel, partie.Classement, partie.Victoire.ToVictoireDefaite(), 1).PointsJoueur1;
            journee.PointsGagnesPerdus += pts;
            partie.PointsGagnesPerdus = pts;
            journee.Parties.Add(partie);
        }

        public static void Add(this JourneeHistoriques journee,Historique historique)
        {
            journee.Historiques.Add(historique);
        }

        public static void AddRange(this JourneeHistoriques journee, IEnumerable< Historique> historiques)
        {
            if (historiques == null) return;
            journee.Historiques.AddRange(historiques);
        }
    }

    public class Journee
    {
        public string Date { get; set; }
        public string Epreuve { get; set; }
        public List<Partie> Parties { get;  } = new List<Partie>();
        public double PointsGagnesPerdus { get; set; } = 0;

    }

    public class JourneeHistoriques
    {
        public List<Historique> Historiques { get; set; } = new List<Historique>();

        public int NombreDeMatch => Historiques.Count();

        public int NombreDeVictoire => Historiques.Where(histo => histo.Victoire.ToLower() == "v").Count();

        public int NombreDeDefaite => NombreDeMatch - NombreDeVictoire;

        public double PointsGagnesPerdu => Historiques.Select(x => x.PointsGagnesPerdus).Sum();

        public double PointsVictoire => Historiques.Where(histo => histo.Victoire.ToLower() == "v").Select(x => x.PointsGagnesPerdus).Sum();

        public double pointsDefaite => PointsGagnesPerdu - PointsVictoire;

        public double MoyennePointParMatch =>Math.Round( PointsGagnesPerdu / (NombreDeMatch==0?1:NombreDeMatch),1);

        public double MoyennePointParVictoire => Math.Round(PointsVictoire /( NombreDeVictoire==0?1 : NombreDeVictoire), 1);

        public double MoyennePointParDefaite => Math.Round(pointsDefaite/ (NombreDeDefaite==0?1: NombreDeDefaite), 1);
    }
}
