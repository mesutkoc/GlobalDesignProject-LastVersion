using MANsys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MANsys.Controllers
{
    public class DefaultController : ApiController
    {
        
     
        public IHttpActionResult Register(RestaurantAccounts model)
        {

            GlobalDesignEntities db = new GlobalDesignEntities();
            db.RestaurantAccounts.Add(new RestaurantAccounts()
            {
                RestaurantName = model.RestaurantName,
                RestaurantMail = model.RestaurantMail,
                RestaurantPassword = model.RestaurantPassword,
                ConfirmPassword = model.ConfirmPassword,
                RestaurantPhone = model.RestaurantPhone,
                RestaurantAdress = model.RestaurantAdress,
                RestaurantCity = model.RestaurantCity,
                RestaurantDistrict = model.RestaurantDistrict
            });
            db.SaveChanges();
            return Ok();
        }
    }
}
