using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.services
{
    public static  class  PingService
    {
        public static int CurrentPhase
        {
            get{
                return GetPhase(DateTime.Now);
                
            }
             
        }

        public static int GetPhase(DateTime dt)
        {
            if (dt.Month > 8 && dt.Month <= 12)
                return 1;
            return 2;
        }

        public static int GetPhase(string s)
        {
            DateTime dt;
            if (DateTime.TryParse(s, out dt))
                return GetPhase(dt);
            return -1;
        }
    }
}
