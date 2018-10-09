using ePing.Api.models;

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace ePing.Api.services
{
    public static class PointServiceExtensions
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
    public static class VictoireDefaiteExtensions
    {
        public static VictoireDefaite ToVictoireDefaite(this string s)
        {
            if (s.Trim().ToLower() == "v") return VictoireDefaite.Victoire;
            if (s.Trim().ToLower() == "d") return VictoireDefaite.Defaite;
            throw new NotSupportedException($"{s} non Supporté. Seulement 'V' et 'D'");
        }
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
        private readonly PointsSettings _settings;

        public PointService(IOptions<PointsSettings> options)
        {
            _settings = options.Value;
            vn.InitializeTabs(_settings.VictoireNormale);
            dn.InitializeTabs(_settings.DefaiteNormale);

            va.InitializeTabs(_settings.VictoireAnormale);
            da.InitializeTabs(_settings.DefaiteAnormale);
        }


        public Dictionary<int, double> GetDico(double diffPoint, VictoireDefaite vd, out string resultat)
        {
            Dictionary<int, double> result;
            switch (vd)
            {
                case VictoireDefaite.Victoire:
                    result = diffPoint > 0 ? vn : va;
                    resultat = diffPoint > 0 ? "Victoire Normale" : "Victoire Anormale";
                    break;
                case VictoireDefaite.Defaite:
                    result = diffPoint > 0 ? dn : da;
                    resultat = diffPoint > 0 ? "Défaite Normale" : "Défaite Anormale";
                    break;
                default:
                    resultat = "??";
                    result = new Dictionary<int, double>();
                    break;
            }
            return result;
        }

        public PointResultat CalculPointsGagnePerdu(double pointsj1, double pointsj2, VictoireDefaite vd, double coeficient = 1.0)
        {
            double diffPoints = (pointsj1 - pointsj2) * (vd == VictoireDefaite.Defaite ? -1 : 1);




            string res1, res2;
            var tab1 = GetDico(diffPoints, vd, out res1);
            var tab2 = GetDico(diffPoints, vd == VictoireDefaite.Victoire ? VictoireDefaite.Defaite : VictoireDefaite.Victoire, out res2);
            var key1 = tab1.Keys.Where(k => k <= Math.Abs(diffPoints)).LastOrDefault();
            var key2 = tab2.Keys.Where(k => k <= Math.Abs(diffPoints)).LastOrDefault();
            return new PointResultat() { PointsJoueur1 = tab1[key2] * coeficient, ResultatJoueur1 = res1, PointsJoueur2 = tab2[key2] * coeficient, ResultatJoueur2 = res2 };
        }

            
        public PointResultat CalculPointsGagnePerdu(Joueur j1, Joueur j2, VictoireDefaite vd, double coeficient = 1.0)
        {
            return CalculPointsGagnePerdu(j1.PointOfficiel, j2.PointOfficiel, vd, coeficient);
            
        }
    }

}
