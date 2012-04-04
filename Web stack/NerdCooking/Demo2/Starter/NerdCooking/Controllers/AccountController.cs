using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NerdCooking.Models;
using Membership = System.Web.Security.Membership;

namespace NerdCooking.Controllers
{
    public class AccountController : Controller
    {
        private IMembershipRepository _membershipRepository;

        public AccountController()
            :this(new MembershipRepository())
        {
            
        }

        public AccountController(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        //
        // GET: /Account/
        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/
        [HttpPost]
        public ActionResult LogOn(string username,string password, bool rememberMe)
        {
            if(!Membership.ValidateUser(username,password))
            {
                this.ModelState.AddModelError(String.Empty,"The username and/or password is invalid.");
                return View();
            }

            FormsAuthentication.SetAuthCookie(username,rememberMe);

            if(Request.QueryString["RedirectUrl"] != null)
            {
                string redirectUrl = Request.QueryString["RedirectUrl"];
                return Redirect(redirectUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register(RegistrationViewModel registrationData)
        {
            try
            {
                var userAccount = Membership.CreateUser(registrationData.UserName, 
                    registrationData.Password,registrationData.EmailAddress);

                var membership = new Models.Membership();
                membership.UserName = userAccount.UserName;
                membership.IsConfirmed = false;
                
                _membershipRepository.InsertOrUpdate(membership);
                _membershipRepository.Save();

                return RedirectToAction("Index", "Home");
            }
            catch(MembershipCreateUserException ex)
            {
                this.ModelState.AddModelError(String.Empty,ex.Message);
                return View();
            }
            catch
            {
                this.ModelState.AddModelError(String.Empty,"A general error ocurred while trying to create your account. Please try again.");
                return View();
            }
        }

        //
        // GET: /Account/LogOff
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
