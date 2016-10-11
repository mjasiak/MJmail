using MJmail.Data;
using MJmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MJmail.Controllers
{
    public class HomeController : Controller
    {
        private readonly MaildbContext _context;

        public HomeController(MaildbContext context)
        {
            _context = context;
        }


        public ActionResult Index()
        {
            return View(_context.Messages.Where(c => c.MailTo == "mjasiak@pl.sii.eu").ToList());
        }
    }
}