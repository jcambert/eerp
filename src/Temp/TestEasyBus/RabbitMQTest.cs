using EasyNetQ;
using EasyNetQ.PollyHandlerRunner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polly;
using System;

namespace TestEasyBus
{
    [TestClass]
    public class RabbitMQTest
    {
        public string ConnectionString => "host=localhost;virtualhost=/;username=jc;password=korben";
        [TestMethod]
        public void BasicTest()
        {
            
            var policy = Policy
                .Handle<Exception>()
                .Retry(3);
            
           // var bus = RabbitHutch.CreateBus(ConnectionString, r => { r.UseMessageHandlerPolicy(policy); });
        }
    }
}
