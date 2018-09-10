using EventBusRabbitMQ;
using System;

namespace BusTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //var con = new DefaultRabbitMQPersistentConnection();
            var bus = Rabbit.CreateBus("host=192.6.2.110;port=5672;user=jc;password=korben");

            bus.Connection.TryConnect();
        }
    }
}
