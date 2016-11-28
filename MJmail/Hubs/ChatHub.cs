using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MJmail.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using MJmail.Data;
using MJMail.Data.Models;
using System.Data.Entity;

namespace MJmail.Hubs
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {      
        static List<UserDetails> ConnectedUsers = new List<UserDetails>();

        public void Connect(string userName)
        {
            string id = Context.ConnectionId;          
            using (var _context = new MaildbContext())
            {
                List<ChatFriend> ChatFriends = new List<ChatFriend>();
                ChatFriends = _context.ChatFriends.Include(c=>c.ApplicationUser).Where(c => c.ApplicationUser.Email == userName && c.ApplicationUser.UserName == userName).ToList();
                if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
                {
                    ConnectedUsers.Add(new UserDetails { ConnectionId = id, UserName = userName });

                    Clients.Caller.onConnected(id, userName, ConnectedUsers, ChatFriends);

                    Clients.AllExcept(id).onNewUserConnected(id, userName);
                }
            }                     
        }

        public void SendPrivateMessage(string sendTo, string msgContent)
        {
            var ToUser = ConnectedUsers.FirstOrDefault(cu => cu.ConnectionId == sendTo);
            var fromUser = ConnectedUsers.FirstOrDefault(cu => cu.ConnectionId == Context.ConnectionId);

            Clients.Caller.sendMessage(ToUser.ConnectionId, fromUser.UserName, msgContent);

            Clients.Client(ToUser.ConnectionId).sendMessage(fromUser.ConnectionId, fromUser.UserName, msgContent);
        }

        public void AddFriend(string friendName)
        {
            ChatFriend chtFr = new ChatFriend();
            var me = Context.User.Identity.Name;
            ApplicationUser friend = new ApplicationUser();
            using (var _context = new MaildbContext())
            {
                var appUser = _context.Users.First(c => c.Email == me && c.UserName == me);
                friend = _context.Users.First(c => c.Email == friendName && c.UserName == friendName);
                chtFr.ApplicationUser = appUser;
                chtFr.Friend = friend.Email;
                _context.ChatFriends.Add(chtFr);
                _context.SaveChanges();
            }
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