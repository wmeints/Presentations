using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auctioneer.Models;
using System.Web.Security;

namespace Auctioneer.Models
{
    /// <summary>
    /// Controller for logging people on to the auctioneer website
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Starts the logon operation
        /// </summary>
        /// <returns>Returns the view for the operation with a new logon form instance</returns>
        public ActionResult LogOn()
        {
            return View(new LogOnForm());
        }

        /// <summary>
        /// Completes the logon operation
        /// </summary>
        /// <param name="form">Form containing the logon details</param>
        /// <returns>Returns the result of the operation</returns>
        [HttpPost]
        public ActionResult LogOn(LogOnForm form)
        {
            if (!TryValidateModel(form))
            {
                return View(form);
            }

            // Save the authentication cookie for the user.
            // NOTE: Don't come crying to me when your website is unsafe. This is for demo purposes only!
            FormsAuthentication.SetAuthCookie(form.Username, false);

            // Redirect the user back to the page he came from
            return Redirect(FormsAuthentication.GetRedirectUrl(form.Username, false));
        }
    }
}
