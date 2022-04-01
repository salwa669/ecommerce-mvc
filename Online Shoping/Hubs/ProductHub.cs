using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Shoping.Hubs
{
    public class ProductHub : Hub
    {
        static Dictionary<string, string> ConnectionID
          = new Dictionary<string, string>();
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
