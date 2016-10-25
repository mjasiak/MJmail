using MJmail.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using MJmail.Models;
using MJMail.Methods.Messages;

namespace MJmail.Controllers
{
    public class MessagesController : Controller
    {
        private readonly MaildbContext _context;

        public MessagesController(MaildbContext context)
        {
            _context = context;
        }

        [ValidateInput(false)]
        public void New(Message msg)
        {
            MessageControl.New(msg, _context);
        }

        public ActionResult Outbox(int? page, string searchString)
        {
            var messages = _context.Messages.Where(c => c.MailFrom == "mjasiak@pl.sii.eu").OrderByDescending(c => c.MailDate).ToList();
            foreach (var msg in messages)
            {
                msg.EncodedID = Encode(msg.ID.ToString());
            }
            int pageNumber = (page ?? 1);

            if (!String.IsNullOrEmpty(searchString))
            {
                messages = messages.Where(s => s.MailTitle.Contains(searchString)
                                        || s.MailContent.Contains(searchString)
                                        || s.MailFrom.Contains(searchString)).ToList();
            }

            return View(messages.ToPagedList(pageNumber, 15));
        }

        public ActionResult Inbox(int? page, string searchString)
        {
            var messages = _context.Messages.Where(c => c.MailTo == "mjasiak@pl.sii.eu").OrderByDescending(c => c.MailDate).ToList();
            foreach(var msg in messages)
            {
                msg.EncodedID = Encode(msg.ID.ToString());
            }
            int pageNumber = (page ?? 1);

            if (!String.IsNullOrEmpty(searchString))
            {
                messages = messages.Where(s => s.MailTitle.Contains(searchString) 
                                        || s.MailContent.Contains(searchString) 
                                        || s.MailFrom.Contains(searchString)).ToList();
            }

            return View(messages.ToPagedList(pageNumber,15));
        }
       
        public PartialViewResult Message(string encodeID)
        {
            var id = Int32.Parse(Decode(encodeID));
            return PartialView("_Message", _context.Messages.Single(c => c.ID == id));
        }

        public void Delete(string[] rows)
        {
            if (rows != null)
            {
                foreach (var row in rows)
                {
                    var id = Int32.Parse(Decode(row));
                    var delRow = _context.Messages.First(c => c.ID == id);
                    _context.Messages.Remove(delRow);
                }
                _context.SaveChanges();
            }           
        }
        #region Crypt
        public string Encode(string encodeMe)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }

        public static string Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }
        #endregion
    }
}