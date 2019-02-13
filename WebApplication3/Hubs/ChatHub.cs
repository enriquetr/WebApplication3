using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebApplication1.Hubs
{
    public class ChatHub : Hub
    {
        public static int MessageCount = 0;

        public void Send(string user, string message)
        {
            MessageCount++;

            Clients.All.addNewMessageToPage(user, MessageCount.ToString() + ") " + message);

        }
    }

}