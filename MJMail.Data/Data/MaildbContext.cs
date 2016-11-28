using Microsoft.AspNet.Identity.EntityFramework;
using MJmail.Models;
using MJMail.Data.Models;
using System.Data.Entity;

namespace MJmail.Data
{
    public class MaildbContext: IdentityDbContext<ApplicationUser>
    {
        public MaildbContext() : base("DbContext")
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatFriend> ChatFriends { get; set; }
    }
}