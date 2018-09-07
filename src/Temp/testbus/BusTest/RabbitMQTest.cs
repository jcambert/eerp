using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;
using System;
using System.Text;

namespace BusTest
{
    [TestClass]
    public class RabbitMQTest
    {
        [TestMethod]
        public void BasicTest()
        {
            var factory = new ConnectionFactory() { HostName = "192.6.2.110",Port=5672,UserName="jc",Password="korben"};
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}
