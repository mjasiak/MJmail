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
            return View(MessageControl.ShowMessages(messages,page,searchString));
        }
        public ActionResult Inbox(int? page, string searchString)
        {
            var messages = _context.Messages.Where(c => c.MailTo == "mjasiak@pl.sii.eu").OrderByDescending(c => c.MailDate).ToList();            
            return View(MessageControl.ShowMessages(messages,page,searchString));
        }
       
        public PartialViewResult Message(string encodeID)
        {
            int id = MessageControl.Decode(encodeID);
            return PartialView("_Message", _context.Messages.Single(c => c.ID == id));
        }

        public void Delete(string[] rows)
        {
            MessageControl.Delete(_context, rows);        
        }
    }
}