﻿using MJmail.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using MJmail.Models;
using MJMail.Methods.Messages;
using MJMail.Data.TDO;
using MJMail.Grid.Paging;

namespace MJmail.Controllers
{
    public class MessagesController : Controller
    {
        private readonly MaildbContext _context;
        private PagingInfo pageInfo = new PagingInfo();

        public MessagesController(MaildbContext context)
        {
            _context = context;
        }

        [ValidateInput(false)]
        public void New(Message msg)
        {
            MessageControl.New(msg, _context);
        }

        //<-- DZIAŁA
        public ActionResult Outbox(int? page, string searchString)
        {
            IEnumerable<Message> messages = MessageControl.ShowMessages(MessageControl.GetAllSentMessages(_context), searchString);
            ViewBag.PagingInfo = pageInfo.SetPagingInfo(page, searchString, 5, messages.Count(), "Outbox", "Messages");
            messages = messages.Skip(pageInfo.pageSize * (page - 1) ?? 0)
                               .Take(pageInfo.pageSize);

            return View(messages);
        }

        // <-- JESZCZE NIE DZIAŁA [OUTBOX]
        //public ActionResult Outbox(int? page, string searchString)
        //{
        //    List<Message> messages = MessageControl.ShowMessages(MessageControl.GetAllSentMessages(_context), searchString);
        //    return View(messages);
        //}

        public ActionResult Inbox(int? page, string searchString)
        {
            IEnumerable<Message> messages = MessageControl.ShowMessages(MessageControl.GetAllReceivedMessages(_context), searchString);
            ViewBag.PagingInfo = pageInfo.SetPagingInfo(page, searchString, 15, messages.Count(), "Inbox", "Messages");
            messages = messages.Skip(pageInfo.pageSize * (page - 1) ?? 0)
                               .Take(pageInfo.pageSize);

            return View(messages);
        }

        public PartialViewResult AdvSearch(AdvancedSearchQuery query)
        {
            IEnumerable<Message> messages = AdvancedSearch.FindMessages(query, _context);            
            return PartialView("_Box", messages);
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
            // <-- ORYGINALNA TESTOWA
        //public ActionResult Test()
        //{
        //    return View(_context.Messages.ToList());
        //}

        public ActionResult Test(int? page, string searchString)
        {
            IEnumerable<Message> messages = MessageControl.ShowMessages(_context.Messages.ToList(), searchString);
            ViewBag.PagingInfo = pageInfo.SetPagingInfo(page,searchString,5,messages.Count(),"Test","Messages");
            messages = messages.Skip(pageInfo.pageSize * (page - 1) ?? 0)
                               .Take(pageInfo.pageSize);

            return View(messages);
        }
    }
}