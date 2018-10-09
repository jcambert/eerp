using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PingPointsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var j1 = new Joueur() { Nom = "Jc", Classement = 5, PointOfficiel = 527 };
            var j2 = new Joueur() { Nom = "Loic", Classement = 5, PointOfficiel = 500 };
            var j3 = new Joueur() { Nom = "Damien", Classement = 6, PointOfficiel = 637 };
            var service = new PointService();

            var key1=service.CalculPointsGagnePerdu(j1, j2,VictoireDefaite.Victoire, 1);
            Assert.IsNotNull(key1);

            var key2 = service.CalculPointsGagnePerdu(j1, j3,VictoireDefaite.Defaite ,1);
            Assert.IsNotNull(key2);

            var key3 = service.CalculPointsGagnePerdu(j1, j3, VictoireDefaite.Victoire, 1);
            Assert.IsNotNull(key3);

            var key4 = service.CalculPointsGagnePerdu(j3, j1, VictoireDefaite.Victoire, 1);
            Assert.IsNotNull(key4);

            var key5 = service.CalculPointsGagnePerdu(j3, j1, VictoireDefaite.Defaite, 1);
            Assert.IsNotNull(key5);
        }
    }
}
