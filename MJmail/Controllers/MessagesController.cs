using MJmail.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using MJmail.Models;
using MJMail.Methods.Messages;
using MJMail.Data.TDO;

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
            IPagedList messages = MessageControl.ShowMessages(MessageControl.GetAllSentMessages(_context), page, searchString);
            if (searchString == null) return View(messages);
            else return PartialView("_Box", messages);
        }

        public ActionResult Inbox(int? page, string searchString)
        {
            IPagedList messages = MessageControl.ShowMessages(MessageControl.GetAllReceivedMessages(_context), page, searchString);
            if (searchString == null) return View(messages);
            else return PartialView("_Box", messages);
        }

        public PartialViewResult AdvSearch(AdvancedSearchQuery query)
        {
            return PartialView("_Box", AdvancedSearch.FindMessages(query, _context));
        }

        public PartialViewResult Box(IPagedList messages)
        {
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