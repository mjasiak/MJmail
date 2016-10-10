using MJmail.Data;
using MJmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailOperator.Messages
{
    public class Receiver
    {
        public static List<Message> Receive(MaildbContext _context)
        {
            return _context.Messages.Where(c => c.MailTo == "mjasiak@pl.sii.eu").ToList();
        }
    }
}
