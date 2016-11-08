using MJmail.Data;
using MJmail.Models;
using MJMail.Data.TDO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJMail.Methods.Messages
{
    public class AdvancedSearch
    {
        public static IPagedList<Message> FindMessages(AdvancedSearchQuery query, MaildbContext context)
        {
            query = SetQueryNull(query);
            List<Message> _messages = null;
            if (query != null)
            {
                _messages = ShowSpecificMessages(context,query);
            }
            else
            {
                _messages = ShowAllMessages(context);
            }

            return _messages.OrderByDescending(m => m.MailDate).Distinct().ToPagedList(1,15);
        }

        #region Helpers
        private static AdvancedSearchQuery SetQueryNull(AdvancedSearchQuery query)
        {
            if (query.MailTo == null && query.MailFrom == null && query.MailTitle == null && query.MailHasWords == null && query.MailDoesntHave == null) query = null;
            return query;
        }

        private static List<Message> ShowAllMessages(MaildbContext context)
        {
            List<Message> messages = new List<Message>();
            messages = context.Messages.ToList();
            
            return Encoding(messages);
        }

        private static List<Message> ShowSpecificMessages(MaildbContext context, AdvancedSearchQuery query)
        {
            List<Message> message = new List<Message>();
            if (query.MailFrom != null) message.AddRange(context.Messages.Where(c => c.MailFrom.Contains(query.MailFrom)));
            if (query.MailTo != null) message.AddRange(context.Messages.Where(c => c.MailTo.Contains(query.MailTo)));
            if (query.MailTitle != null) message.AddRange(context.Messages.Where(c => c.MailTitle.Contains(query.MailTitle)));
            if (query.MailHasWords != null) message = HasWords(message, context, query.MailHasWords);
            if (query.MailDoesntHave != null)
            {
                if(query.MailFrom == null && query.MailTo == null && query.MailTitle == null && query.MailHasWords == null)
                {
                    message = context.Messages.ToList();
                    message = DoesntHave(message, context, query.MailDoesntHave);
                }
                else message = DoesntHave(message, context, query.MailDoesntHave);
            }

            return Encoding(message);
        }

        private static List<Message> HasWords(List<Message> messages, MaildbContext context, string MailHasWords)
        {
            string[] hasWords = MailHasWords.Split(' ');

            foreach(var word in hasWords)
            {
                messages.AddRange(context.Messages.Where(c => c.MailContent.Contains(word) || c.MailTitle.Contains(word)));
            }

            return messages;
        }

        private static List<Message> DoesntHave(List<Message> messages, MaildbContext context, string MailDoesntHave)
        {
            string[] doesntHave = MailDoesntHave.Split(' ');

            foreach (var word in doesntHave)
            {
                Message toRemove = messages.First(c => c.MailContent.Contains(word) || c.MailTitle.Contains(word));
                if (toRemove != null) messages.Remove(toRemove);
            }

            return messages;
        }
        #endregion

        #region Crypting
        private static List<Message> Encoding(List<Message> messages)
        {
            foreach (var msg in messages)
            {
                msg.EncodedID = MessageControl.Encode(msg.ID.ToString());
            }

            return messages;
        }
        #endregion
    }
}
