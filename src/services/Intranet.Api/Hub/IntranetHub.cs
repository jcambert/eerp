using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Intranet.Api
{
    
    public class IntranetHub:Hub
    {
        public IntranetHub()
        {
            
        }
        public override Task OnConnectedAsync()
        {
            this.Clients.All.SendAsync("A new connection append");
            return base.OnConnectedAsync();
        }
    }
}
