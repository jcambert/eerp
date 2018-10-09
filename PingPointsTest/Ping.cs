using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPointsTest
{
    public static class Extensions
    {
        public static void InitializeTabs(this Dictionary<int, double> v, string s)
        {
            if (v == null) v = new Dictionary<int, double>();
            var ss = s.Split('*');
            for (int i = 0; i < ss.Length; i += 2)
            {
                v[Int32.Parse(ss[i])] = Double.Parse(ss[i + 1]);
            }
        }
    }

    public class Joueur
    {
        public string Nom { get; set; }
        public int Classement { get; set; }
        public double PointOfficiel { get; set; }
    }
    public enum VictoireDefaite
    {
        Victoire,
        Defaite
    }
    public class PointResultat
    {
        public string ResultatJoueur1 { get; set; }
        public string ResultatJoueur2 { get; set; }

        public double PointsJoueur1 { get; set; }
        public double PointsJoueur2 { get; set; }
    }
    public class PointService
    {
        Dictionary<int, double> vn = new Dictionary<int, double>();
        Dictionary<int, double> dn = new Dictionary<int, double>();
        Dictionary<int, double> va = new Dictionary<int, double>();
        Dictionary<int, double> da = new Dictionary<int, double>();
        public PointService()
        {

            vn.InitializeTabs("0*6*24*5.5*49*5*99*4*149*3*199*2*299*1*399*0.5*5000*0");
            dn.InitializeTabs("0*-5*24*-4.5*49*-4*99*-3*149*-2*199*1*299*-0.5*5000*0");

            va.InitializeTabs("0*6*24*7*49*8*99*10*149*13*199*17*299*22*399*28*5000*35");
            da.InitializeTabs("0*-5*24*-6*49*-7*99*-8*149*-10*199*-12.5*299*-16*399*-20*5000*-25");
        }


        public Dictionary<int,double> GetDico(double diffPoint,VictoireDefaite vd,out string resultat)
        {
            Dictionary<int, double> result;
            switch (vd)
            {
                case VictoireDefaite.Victoire:
                    result= diffPoint > 0 ? vn : va;
                    resultat = diffPoint > 0 ? "Victoire Normale" : "Victoire Anormale";
                    break;
                case VictoireDefaite.Defaite:
                    result= diffPoint > 0 ? dn : da;
                    resultat = diffPoint >0 ? "Défaite Normale" : "Défaite Anormale";
                    break;
                default:
                    resultat = "??";
                    result = new Dictionary<int, double>();
                    break;
            }
            return result;
        }

        public PointResultat CalculPointsGagnePerdu(Joueur j1, Joueur j2,VictoireDefaite vd, double coeficient = 1.0)
        {
            int sameClasse = j1.Classement - j2.Classement;
            double diffPoints = (j1.PointOfficiel - j2.PointOfficiel)* (vd == VictoireDefaite.Defaite?-1:1);

            

            string res1, res2;
            var tab1 = GetDico(diffPoints , vd,out res1);
            var tab2 = GetDico(diffPoints, vd == VictoireDefaite.Victoire ? VictoireDefaite.Defaite: VictoireDefaite.Victoire, out res2);
            var key1 = tab1.Keys.Where(k => k <= Math.Abs(diffPoints)).LastOrDefault();
            var key2 = tab2.Keys.Where(k => k <= Math.Abs(diffPoints)).LastOrDefault();
            return new PointResultat() {PointsJoueur1=tab1[key2]*coeficient,ResultatJoueur1=res1,PointsJoueur2=tab2[key2]*coeficient,ResultatJoueur2=res2 };
        }
    }
}
