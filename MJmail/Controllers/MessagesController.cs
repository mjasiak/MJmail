using MJmail.Data;
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
        private IMessageControl _msgCntrl;
        private IAdvancedSearch _advSearch;
        private IPagingInfo _pageInfo;

        public MessagesController(MaildbContext context, IMessageControl msgCntrl, IAdvancedSearch advSearch, IPagingInfo pageInfo)
        {
            _context = context;
            _msgCntrl = msgCntrl;
            _advSearch = advSearch;
            _pageInfo = pageInfo;
        }

        [ValidateInput(false)]
        public void New(Message msg)
        {
            _msgCntrl.New(msg, _context);
        }

        public ActionResult Outbox(int? page, string searchString)
        {            
            return View(getMessages(getAction(), page,searchString));
        }

        public ActionResult Inbox(int? page, string searchString)
        {           
            return View(getMessages(getAction(), page,searchString));
        }

        public PartialViewResult AdvSearch(AdvancedSearchQuery query)
        {
            IEnumerable<Message> messages = _advSearch.FindMessages(query, _context);            
            return PartialView("_Box", messages);
        }

        //currently unused
        public PartialViewResult Box(IPagedList messages)
        {
            return PartialView("_Box", messages);
        }

        public PartialViewResult Message(string encodeID)
        {
            int id = _msgCntrl.Decode(encodeID);
            return PartialView("_Message", _context.Messages.Single(c => c.ID == id));
        }

        public void Delete(string[] rows)
        {
            _msgCntrl.Delete(_context, rows);
        }

        public ActionResult Test(int? page, string searchString)
        {
            return View(getMessages(getAction(),page,searchString));
        }

        #region FuncT
        private IEnumerable<Message> getMessages(string action, int?page, string searchString)
        {
            return pagingControl(action, page, searchString);
        }
        private IEnumerable<Message> pagingControl(string action, int? page, string searchString)
        {
            IEnumerable<Message> messages = prepareMessages(action, searchString);
            ViewBag.PagingInfo = _pageInfo.SetPagingInfo(page, searchString, 15, messages.Count(), "Inbox", "Messages");
            messages = messages.Skip(_pageInfo.pageSize * (page - 1) ?? 0)
                               .Take(_pageInfo.pageSize);

            return messages;
        }
        private IEnumerable<Message> prepareMessages(string action, string searchString)
        {
            Func<MaildbContext,List<Message>> box;
            if (action.Equals("Inbox")) box = _msgCntrl.GetAllReceivedMessages;
            else if (action.Equals("Outbox")) box = _msgCntrl.GetAllSentMessages;
            else box = _msgCntrl.GetAllMessages;

            return _msgCntrl.ShowMessages(box(_context), searchString);
        }
        #endregion

        #region Helpers
        private string getAction()
        {
            return ControllerContext.RouteData.Values["action"].ToString();
        }
        #endregion
    }
}