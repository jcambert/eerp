using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Parser
{
    public class ParseException : Exception
    {
        public ParseException(string message)
            : base(message)
        {
        }
    }
}
