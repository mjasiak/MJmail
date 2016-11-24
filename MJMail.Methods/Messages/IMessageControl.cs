using MJmail.Data;
using MJmail.Models;
using MJMail.Data.Models;
using System.Collections.Generic;

namespace MJMail.Methods.Messages
{
    public interface IMessageControl
    {
        void New(Message msg, MaildbContext context,ApplicationUser appUser);
        List<Message> ShowMessages(List<Message> messages, string searchString);
        void Delete(MaildbContext _context, string[] rows);
        string Encode(string encodeMe);
        int Decode(string decodeMe);
        List<Message> GetAllSentMessages(MaildbContext _context, ApplicationUser appUser);
        List<Message> GetAllReceivedMessages(MaildbContext _context, ApplicationUser appUser);
        List<Message> GetAllMessages(MaildbContext _context, ApplicationUser appUser);
    }
}
