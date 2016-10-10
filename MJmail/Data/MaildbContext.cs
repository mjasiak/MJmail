using MJmail.Models;
using System.Data.Entity;

namespace MJmail.Data
{
    public class MaildbContext: DbContext
    {
        public MaildbContext() : base("DbContext")
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}