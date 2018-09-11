using EventBusRabbitMQ;
using EventBusRabbitMQ.ConnectionString;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using Ninject;
namespace BusTest
{
    [TestClass]
    public class RabbitMQTest
    {
        static string ConnectionString=>  "host=192.6.2.110;port=5672;username=jc;password=korben";
        [TestMethod]
        public void TestConnectionString()
        {
            var parser=Rabbit.Kernel.Get<IConnectionStringParser>();
            var conf = parser.Parse(ConnectionString);
            Assert.AreEqual(conf.Host,"192.6.2.110");
            Assert.AreEqual(conf.Port, 5672);
            Assert.AreEqual(conf.UserName, "jc");
            Assert.AreEqual(conf.Password, "korben");
            Assert.AreEqual(conf.VirtualHost, "/");
        }

        [TestMethod]
        public void TestCreateSimpleBus()
        {
            var bus = Rabbit.CreateBus(ConnectionString);
            Assert.IsNotNull(bus);
        }
    }
}
