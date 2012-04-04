using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auctioneer.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Displays the page for unhandled exceptions
        /// </summary>
        /// <returns></returns>
        public ActionResult Unhandled()
        {
            return View();
        }
        
        /// <summary>
        /// Displays the page not found error
        /// </summary>
        /// <returns></returns>
        public ActionResult PageNotFound()
        {
            return View();
        }

        /// <summary>
        /// Displays the page for access denied scenarios
        /// </summary>
        /// <returns></returns>
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
