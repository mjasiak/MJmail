using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MJmail.Helpers;

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

        public void SendMessage()
        {

        }
    }
}