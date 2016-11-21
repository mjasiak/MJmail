using MJmail.Data;
using MJmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Methods.Messages
{
    public interface IMessageControl
    {
        void New(Message msg, MaildbContext context);
        List<Message> ShowMessages(List<Message> messages, string searchString);
        void Delete(MaildbContext _context, string[] rows);
        string Encode(string encodeMe);
        int Decode(string decodeMe);
        List<Message> GetAllSentMessages(MaildbContext _context);
        List<Message> GetAllReceivedMessages(MaildbContext _context);
    }
}
