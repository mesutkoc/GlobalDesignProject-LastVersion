using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MANsys.Controllers
{
    public class RegisterTableAndWorkerController : Controller
    {
        public ActionResult RegisterTableAndWorker()
        {
            return View();
        }
        /*[HttpPost]
        public ActionResult RegisterTableAndWorker(RestaurantCategories category)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44382/api/Restaurant");

                var insertrec = hc.PostAsJsonAsync<RestaurantCategories>("RegisterTableAndWorker", category);
                insertrec.Wait();

                var saverec = insertrec.Result;
                if (saverec.IsSuccessStatusCode)
                {
                    ViewBag.message = "The user " + category.CategoryName + "is inserted succesfully";
                }
            }

            return View();
        }*/
        [HttpPost]
        public ActionResult RegisterTableAndWorker(RestaurantTable table)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44382/api/Restaurant");

                var insertrec = hc.PostAsJsonAsync<RestaurantTable>("RegisterTableAndWorker", table);
                insertrec.Wait();

                var saverec = insertrec.Result;
                if (saverec.IsSuccessStatusCode)
                {
                    ViewBag.message = "The user " + table.TableName + "is inserted succesfully";
                }
            }

            return View();
        }
      

        [HttpPost]
        public ActionResult RegisterTableAndWorker(RestaurantItems item)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44382/api/Restaurant");

                var insertrec = hc.PostAsJsonAsync<RestaurantItems>("RegisterTableAndWorker", item);
                insertrec.Wait();

                var saverec = insertrec.Result;
                if (saverec.IsSuccessStatusCode)
                {
                    ViewBag.message = "The user " + item.ItemName + "is inserted succesfully";
                }
            }

            return View();
        }

        //We avoid this because conflict.
        //public ActionResult RegisterTableAndWorker(RestaurantProcess process)
        //{
        //    if (ModelState.IsValid)
         //   {
        //        HttpClient hc = new HttpClient();
         //       hc.BaseAddress = new Uri("https://localhost:44382/api/Restaurant");

         //       var insertrec = hc.PostAsJsonAsync<RestaurantProcess>("RegisterTableAndWorker", process);
         //       insertrec.Wait();

           //     var saverec = insertrec.Result;
          //      if (saverec.IsSuccessStatusCode)
          //      {
            //        ViewBag.message = "The user " + process.ProcessName + "is inserted succesfully";
            //    }
           // }

            //return View();
        //}

    }
}