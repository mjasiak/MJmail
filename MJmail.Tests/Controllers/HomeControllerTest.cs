using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MJmail;
using MJmail.Controllers;
using MJmail.Data;

namespace MJmail.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private readonly MaildbContext _context;

        public HomeControllerTest(MaildbContext context)
        {
            _context = context;
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_context);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
