using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Models
{
    public class DashboardViewModel
    {
        public UserViewModel User { get; internal set; }
        public string Token { get; internal set; }
        public ApiSettings ApiSettings { get; internal set; }
        public string ViewingClub { get; internal set; }
    }
}
