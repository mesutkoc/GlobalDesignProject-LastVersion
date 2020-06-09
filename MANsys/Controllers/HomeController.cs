using MANsys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MANsys.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MainPage", "Home");
            }
            ViewBag.ReturnUrl = returnUrl ?? Url.Action("Index", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult Index(RestaurantAccounts user)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44382/api/Default");

                var insertrec = hc.PostAsJsonAsync<RestaurantAccounts>("Default", user);
                insertrec.Wait();

                var saverec = insertrec.Result;
                if (saverec.IsSuccessStatusCode)
                {
                    ViewBag.message = "The user " + user.RestaurantName + "is inserted succesfully";
                }
            }
            Response.Cookies.Add(new HttpCookie("RestMail", user.RestaurantMail));
            Response.Cookies.Add(new HttpCookie("RestName", user.RestaurantName));

            return RedirectToAction("RegisterTableAndWorker", "RegisterTableAndWorker");
        }

        public ActionResult MainPage()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult TablePage()
        {
            return View();
        }
        public ActionResult CustomerSide()
        {
            return View();
        }
    }
}