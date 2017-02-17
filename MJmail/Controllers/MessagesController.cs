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
using MJMail.Data.Models;

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

        [Authorize]
        [ValidateInput(false)]
        public void New(Message msg)
        {
            _msgCntrl.New(msg, _context,getUser());
        }

        [Authorize]
        public ActionResult Outbox(int? page, string searchString)
        {
            return View(getMessages(getAction(), page,searchString,getUser()));
        }

        [Authorize]
        public ActionResult Inbox(int? page, string searchString)
        {

            return View(getMessages(getAction(), page,searchString, getUser()));
        }

        [Authorize]
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

        [Authorize]
        public PartialViewResult Message(string encodeID)
        {
            int id = _msgCntrl.Decode(encodeID);
            return PartialView("_Message", _context.Messages.Single(c => c.ID == id));
        }

        [Authorize]
        public void Delete(string[] rows)
        {
            _msgCntrl.Delete(_context, rows);
        }

        [Authorize]
        public ActionResult Test(int? page, string searchString)
        { 
            return View(getMessages(getAction(),page,searchString,getUser()));
        }

        #region FuncT
        private IEnumerable<Message> getMessages(string action, int?page, string searchString, ApplicationUser appUser)
        {
            return pagingControl(action, page, searchString, appUser);
        }
        private IEnumerable<Message> pagingControl(string action, int? page, string searchString, ApplicationUser appUser)
        {
            IEnumerable<Message> messages = prepareMessages(action, searchString, appUser);
            ViewBag.PagingInfo = _pageInfo.SetPagingInfo(page, searchString, 15, messages.Count(), "Inbox", "Messages");
            messages = messages.Skip(_pageInfo.pageSize * (page - 1) ?? 0)
                               .Take(_pageInfo.pageSize);

            return messages;
        }
        private IEnumerable<Message> prepareMessages(string action, string searchString,ApplicationUser appUser)
        {
            Func<MaildbContext,ApplicationUser,List<Message>> box;
            if (action.Equals("Inbox")) box = _msgCntrl.GetAllReceivedMessages;
            else if (action.Equals("Outbox")) box = _msgCntrl.GetAllSentMessages;
            else box = _msgCntrl.GetAllMessages;

            return _msgCntrl.ShowMessages(box(_context,appUser), searchString);
            //SomeChanges
        }
        #endregion

        #region Helpers
        private string getAction()
        {
            return ControllerContext.RouteData.Values["action"].ToString();
        }
        private ApplicationUser getUser()
        {
            return _context.Users.First(c => c.UserName == User.Identity.Name);
            //return new ApplicationUser();
        }
        #endregion
    }
}