using ePing.Api.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    public static class JourneeExtensions
    {
        public static void AddPartie(this Journee journee,Joueur joueur, Partie partie,PointService service)
        {
            var pts=service.CalculPointsGagnePerdu(joueur.PointOfficiel, partie.Classement, partie.Victoire.ToVictoireDefaite(), 1).PointsJoueur1;
            journee.PointsGagnesPerdus += pts;
            partie.PointsGagnesPerdus = pts;
            journee.Parties.Add(partie);
        }
    }

    public class Journee
    {
        public string Date { get; set; }
        public string Epreuve { get; set; }
        public List<Partie> Parties { get;  } = new List<Partie>();
        public double PointsGagnesPerdus { get; set; } = 0;

    }
}
