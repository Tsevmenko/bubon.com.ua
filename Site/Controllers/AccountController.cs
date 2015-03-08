using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Site.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public class FormsAuthProvider
        {
            private class IUserBL
            {
                public string name = "admin";
                public string pass = "pass";
            }
            private IUserBL _user;
            public FormsAuthProvider()//IUserBL user)
            {
                _user = new IUserBL();
            }
            public bool Authenticate(string username, string password)
            {
                bool result = false;

                if (username == _user.name && password == _user.pass)
                    result = true;
                //var result = _user.GetUser().FirstOrDefault(p => p.Email == username && p.Password == password);
                if (result)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                }
                return result;//(result) ? true : false;
            }
        }

        FormsAuthProvider authProvider;
        private class IUserBL
        {
            public string name = "admin";
            public string pass = "pass";
        }
        private IUserBL _user;
        public AccountController()//IUserBL user)
        {
            //_user = user;
            authProvider = new FormsAuthProvider();//_user);
        }
        public ViewResult LogOn()
        {
            return View(new LogOnViewModel());
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                else
                {
                    // Неверное имя пользователя или пароль
                    ModelState.AddModelError("", "Неверное имя пользователя или пароль.");
                    return View();
                }
            }
            else
            {
                return View(new LogOnViewModel());
            }
        }
    }
}
