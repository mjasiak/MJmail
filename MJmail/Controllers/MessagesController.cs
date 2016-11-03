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

        public ActionResult Outbox()
        {                      
            return View();
        }

        public ActionResult Inbox()
        {            
            return View();
        }

        public PartialViewResult Box(int wut ,int? page, string searchString)
        {
            IPagedList messages;
            if (wut == 1) messages = MessageControl.ShowMessages(MessageControl.GetAllReceivedMessages(_context), page, searchString);
            else messages = MessageControl.ShowMessages(MessageControl.GetAllSentMessages(_context), page, searchString);
            return PartialView("_Box", messages);
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