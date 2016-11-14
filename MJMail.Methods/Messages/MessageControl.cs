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

        public static List<Message> ShowMessages(List<Message> messages, string searchString)
        {
            foreach (var msg in messages)
            {
                msg.EncodedID = Encode(msg.ID.ToString());
            }

            return searchMethod(messages, searchString);
        }

        public static void Delete(MaildbContext _context, string[] rows)
        {
            if (rows != null)
            {
                foreach (var row in rows)
                {
                    int id = Decode(row);
                    var delRow = _context.Messages.First(c => c.ID == id);
                    _context.Messages.Remove(delRow);
                }
                _context.SaveChanges();
            }
        }

        #region Crypt
        public static string Encode(string encodeMe)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }
        public static int Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            int encodedID = Int32.Parse(System.Text.Encoding.UTF8.GetString(encoded));
            return encodedID;
        }
        #endregion
        #region Helpers
        public static List<Message> GetAllReceivedMessages(MaildbContext _context)
        {
            return _context.Messages.Where(c => c.MailTo == "mjasiak@pl.sii.eu").OrderByDescending(c => c.MailDate).ToList();
        }
        public static List<Message> GetAllSentMessages(MaildbContext _context)
        {
            return _context.Messages.Where(c => c.MailFrom == "mjasiak@pl.sii.eu").OrderByDescending(c => c.MailDate).ToList();
        }
        #endregion
        #region Search
        private static List<Message> searchMethod(List<Message> messages, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                messages = messages.Where(s => s.MailTitle.Contains(searchString)
                                        || s.MailContent.Contains(searchString)
                                        || s.MailFrom.Contains(searchString)
                                        || (s.MailFromName!=null && s.MailFromName.Contains(searchString))
                                        || s.MailTo.Contains(searchString)
                                        || (s.MailToName!=null && s.MailToName.Contains(searchString))).ToList();
            }

            return messages;
        }
        #endregion
    }
}
