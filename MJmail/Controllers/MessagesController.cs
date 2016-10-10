using MJmail.Data;
using MJmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MJmail.Controllers
{
    public class MessagesController : Controller
    {
        private readonly MaildbContext _context;

        public MessagesController(MaildbContext context)
        {
            _context = context;
        }

        // GET: Messages
        public ActionResult Outbox()
        {         
            return View(ReceiveMessages());
        }

        #region Helpers
        private List<Message> ReceiveMessages()
        {
            return _context.Messages.Where(c => c.MailTo == "mjasiak@pl.sii.eu").ToList();
        }
        #endregion
    }
}