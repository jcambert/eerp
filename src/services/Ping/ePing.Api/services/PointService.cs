using ePing.Api.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.services
{
    public class PointService
    {
        public PointService()
        {
           
        }
        public void CalculPointsGagnePerdu(Joueur j1, Joueur j2, double coeficient = 1.0)
        {
            int sameClasse = j1.Classement - j2.Classement;
            double diffPoints = j1.PointOfficiel - j2.PointOfficiel;


        }
    }
}
