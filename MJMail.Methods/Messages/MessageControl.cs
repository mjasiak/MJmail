using MJmail.Data;
using MJmail.Models;
using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Methods.Messages
{
    public class MessageControl
    {
        public static void New(Message msg, MaildbContext context)
        {
            msg.MailDate = DateTime.Now;
            msg.MailFrom = "mjasiak@pl.sii.eu";
            context.Messages.Add(msg);
            context.SaveChanges();
        }

        public static IPagedList ShowReceivedMessages()
        {

        }
    }
}
