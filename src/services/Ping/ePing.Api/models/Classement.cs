
using System;
using System.Text.RegularExpressions;

namespace ePing.Api
{
    using ePing.Api.models;
    using System.Collections.Generic;

    static class ClassementExtension
    {
        public static string CalculDate(this Classement c)
        {
            Regex regex = new Regex(@"\D+");
            string fmt = "MMM yyyy";
            if (c.Phase <=0)
            {
                var tmp = c.Phase;
                var now = DateTime.Now;
                c.Phase = now.Month <= 6 ? 2 : 1;
                c.Saison = c.Phase==1? $"Saison {now.Year} / {now.Year + 1 }" : $"Saison {now.Year-1} / {now.Year }";
                if (tmp == -1)
                    return new DateTime(now.Year, 7, 31).ToString(fmt);

                return  DateTime.Now.ToString(fmt);
            }
            var annees = regex.Split(c.Saison);
           
            return c.Phase == 1 ? new DateTime(int.Parse(annees[c.Phase]),7,31).ToString(fmt) : new DateTime(int.Parse(annees[c.Phase]),1,1).ToString(fmt);
        }

        public static void AddClassementActuel(this List<Classement> histo, Joueur joueur)
        {
            if (histo == null || joueur == null) return;
            Classement cl = new Classement() { Phase = -1, Point = joueur.PointDebut };
            histo.Add(cl);
            cl = new Classement() { Phase = 0, Point = joueur.Point };
            histo.Add(cl);
        }
    }
}
namespace ePing.Api.models
{

    public class Classement : Trackable
    {
        public string Date => this.CalculDate();
        public string Saison { get; set; }
        public int Phase { get; set; }
        public double Point { get; set; }
    }
}
