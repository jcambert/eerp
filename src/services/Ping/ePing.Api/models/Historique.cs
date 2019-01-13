namespace ePing.Api.models
{
  
    public class Historique : Trackable
    {


        public string Licence { get; set; }

        public string LicenceAdversaire { get; set; }

        public string Victoire { get; set; }

        public string Journee { get; set; }

        public string CodeChampionnat { get; set; }

        public string Date { get; set; }

        public int Phase
        {
            get;
            set;
        }

        public string SexeAdversaire { get; set; }

        public string NomPrenomAdversaire { get; set; }

        public double PointsGagnesPerdus { get; set; }

        public string CoefficientEpreuve { get; set; }

        public string ClassementAdversaire { get; set; }

        //[key]
        public string IdPartie { get; set; }


    }
}
