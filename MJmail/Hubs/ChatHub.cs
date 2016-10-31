using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MJmail.Helpers;
using System.Threading.Tasks;

namespace MJmail.Hubs
{
    public class ChatHub : Hub
    {
        static List<UserDetails> ConnectedUsers = new List<UserDetails>();
        static List<PrivMessage> MessageBox = new List<PrivMessage>();

        public void Connect(string userName)
        {
            string id = Context.ConnectionId;

            if(ConnectedUsers.Count(x=>x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserDetails { ConnectionId = id, UserName = userName });

                Clients.Caller.onConnected(id,userName,ConnectedUsers);

                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        public void SendMessage(string sendTo, string msgContent)
        {

        }

        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
                var obiekt = ConnectedUsers.First(cu => cu.ConnectionId == Context.ConnectionId);
                if (obiekt != null)
                {
                    ConnectedUsers.Remove(obiekt);
                    Clients.All.onUserDisconnected(obiekt.ConnectionId);
                }
            }
            catch { }
        
            return base.OnDisconnected(stopCalled);
        }
    }
}