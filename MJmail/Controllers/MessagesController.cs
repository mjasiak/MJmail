using MJmail.Data;
using MJmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [ValidateInput(false)]
        public void New(Message msg)
        {
            msg.MailDate = DateTime.Now;
            msg.MailFrom = "mjasiak@pl.sii.eu";
            _context.Messages.Add(msg);
            _context.SaveChanges();
        }

        public ActionResult Outbox()
        {         
            return View(_context.Messages.Where(c => c.MailFrom == "mjasiak@pl.sii.eu").OrderByDescending(c=>c.MailDate).ToList());
        }

        public ActionResult Inbox()
        {
            return View(_context.Messages.Where(c => c.MailTo == "mjasiak@pl.sii.eu").OrderByDescending(c => c.MailDate).ToList());
        }

        public PartialViewResult Message(int id)
        {
            return PartialView("_Message", _context.Messages.Single(c => c.ID == id));
        }

        public void Delete(int[] rows)
        {
            if (rows != null)
            {
                foreach (var row in rows)
                {
                    var delRow = _context.Messages.First(c => c.ID == row);
                    _context.Messages.Remove(delRow);
                }
                _context.SaveChanges();
            }           
        }
    }
}