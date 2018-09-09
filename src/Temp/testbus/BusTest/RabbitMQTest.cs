using EventBusRabbitMQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;
using System;
using System.Text;

namespace BusTest
{
    [TestClass]
    public class RabbitMQTest
    {
        public IConnectionFactory ConnectionFactory => new ConnectionFactory() { HostName = "192.168.0.23", Port = 5672, UserName = "jc", Password = "korben" };

        [TestMethod]
        public void BasicTest()
        {
            var factory =  ConnectionFactory;
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "BasicTest",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    string message = "Hello World! from basic Test";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine($" [x] Sent {message}");
                }
            }
        }

        [TestMethod]
        public void PersistentConnectionTest()
        {
            var factory = ConnectionFactory;
            using (var connection = new DefaultRabbitMQPersistentConnection(factory, new UnitTestLogger<DefaultRabbitMQPersistentConnection>(Console.WriteLine)))
            {
                connection.TryConnect();
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "PersistentConnectionTest",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    string message = "Hello World! from PersistentConnection Test";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine($" [x] Sent {message}");
                }
            }  
        }

        [TestMethod]
        public void PersistentConnectionBlockingTest()
        {
            var factory = ConnectionFactory;
            using (var connection = new DefaultRabbitMQPersistentConnection(factory, new UnitTestLogger<DefaultRabbitMQPersistentConnection>(Console.WriteLine)))
            {
                connection.TryConnect();
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "PersistentConnectionBlockingTest",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                    channel.ConfirmSelect();
                    string message = "Hello World! from PersistentConnection Test";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine($" [x] Sent {message}");
                }
            }
        }
    }
}
