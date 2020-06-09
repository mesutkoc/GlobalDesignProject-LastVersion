using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MANsys.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MainPage", "Home");
            }
            ViewBag.ReturnUrl = returnUrl ?? Url.Action("Login", "Account");
            return View();
        }

        [HttpPost]
        public ActionResult Login(RestaurantAccounts model, string returnUrl)
        {
            GlobalDesignEntities db = new GlobalDesignEntities();
            RestaurantAccounts dataItem = db.RestaurantAccounts.FirstOrDefault(x => x.RestaurantMail == model.RestaurantMail && x.RestaurantPassword == model.RestaurantPassword);
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.RestaurantMail, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("MainPage", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid User or Password");
                return View();

            }
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}