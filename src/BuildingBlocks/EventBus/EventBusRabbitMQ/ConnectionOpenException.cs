using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
    public class ConnectionOpenException: RabbitMQClientException
    {
        public ConnectionOpenException():base("Connexion is opened")
        {

        }
    }
}
