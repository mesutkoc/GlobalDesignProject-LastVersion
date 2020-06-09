using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using System.Configuration;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data.Entity;
using MANsys.Models;
using System.Data.Entity.Core.Metadata.Edm;
using System.Security.Cryptography;

namespace MANsys.Controllers
{
    public class RestaurantController : ApiController
    {

        [Route("api/restaurant/getall")]
        [HttpGet]
        public IEnumerable<RestaurantAccounts> Get()
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                return entities.RestaurantAccounts.ToList();
            }
        }

        [Route("api/restaurant/graph/daily")]
        [HttpGet]
        public IEnumerable<TableIncome> getIncomesDaily()
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                return entities.TableIncome.ToList();
            }
        }

        [Route("api/restaurant/income")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] TableIncome tableIncome)
        {

            try
            {
                using (GlobalDesignEntities entities = new GlobalDesignEntities())
                {

                    entities.TableIncome.Add(tableIncome);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, tableIncome);
                    message.Headers.Location = new Uri(Request.RequestUri + tableIncome.Id.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/restaurant/process")]
        [HttpPost]
        public HttpResponseMessage Post(RestaurantProcess process)
        {
            try
            {
                using(GlobalDesignEntities entities =new GlobalDesignEntities())
                {
                    entities.RestaurantProcess.Add(process);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, process);
                    message.Headers.Location = new Uri(Request.RequestUri + process.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/restaurant/category")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] RestaurantCategories categories)
        {

            try
            {
                using (GlobalDesignEntities entities = new GlobalDesignEntities())
                {

                    entities.RestaurantCategories.Add(categories);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, categories);
                    message.Headers.Location = new Uri(Request.RequestUri + categories.Id.ToString());



                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/Restaurant/cusOrder")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] CustomerService customerService)
        {

            try
            {
                using (GlobalDesignEntities entities = new GlobalDesignEntities())
                {

                    entities.CustomerService.Add(customerService);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, customerService);
                    message.Headers.Location = new Uri(Request.RequestUri + customerService.Id.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("api/Restaurant/CustomerReservation")]
        [HttpPost]
        public HttpResponseMessage PostCustomerReservation([FromBody] CustomerOldBooking oldBooking)
        {

            try
            {
                using (GlobalDesignEntities entities = new GlobalDesignEntities())
                {

                    entities.CustomerOldBooking.Add(oldBooking);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, oldBooking);
                    message.Headers.Location = new Uri(Request.RequestUri + oldBooking.Id.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/Restaurant/CusTableDelete")]
        [HttpDelete]
        public HttpResponseMessage DeleteTableOrder(string tablename)
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                entities.CustomerService.RemoveRange(entities.CustomerService.Where(e => e.TableName == tablename));
               
                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }


        [Route("api/restaurant/table")]
        [HttpPost]
        public HttpResponseMessage Post(RestaurantTable tables)
        {
            try
            {
                GlobalDesignEntities db = new GlobalDesignEntities();
                db.RestaurantTable.Add(new RestaurantTable()
                {
                    Id = tables.Id,
                    RestaurantMail = tables.RestaurantMail,
                    TableName = tables.TableName,
                    TableSeatNumber = tables.TableSeatNumber,
                    TableBooking = tables.TableBooking
                });
                db.SaveChanges();
                var message = Request.CreateResponse(HttpStatusCode.Created, tables);
                message.Headers.Location = new Uri(Request.RequestUri + tables.Id.ToString());
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/Restaurant/Item")]
        [HttpPost]
        public HttpResponseMessage Post(RestaurantItems items)
        {
            try
            {
                GlobalDesignEntities db = new GlobalDesignEntities();
                db.RestaurantItems.Add(new RestaurantItems()
                {
                    RestaurantMail = items.RestaurantMail,
                    ItemCategory = items.ItemCategory,
                    ItemName = items.ItemName,
                    ItemPrice = items.ItemPrice
                });
                db.SaveChanges();
                var message = Request.CreateResponse(HttpStatusCode.Created, items);
                message.Headers.Location = new Uri(Request.RequestUri + items.Id.ToString());
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        

        [Route("api/Restaurant/GetCat")]
        [HttpGet]
        public IEnumerable<RestaurantCategories> Get(string mail)
        {

            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                return entities.RestaurantCategories.Where(s => s.RestaurantMail == mail).ToList();

            }
        }

        [Route("api/Restaurant/GetName")]
        [HttpGet]
        public HttpResponseMessage GetName(string mail)
        {
            using (GlobalDesignEntities entites = new GlobalDesignEntities())
            {
                var ne = entites.RestaurantAccounts.Where(name => name.RestaurantMail == mail).Select(name => name.RestaurantName).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, ne);
            }
        }

        [Route("api/Restaurant/GetMail")]
        [HttpGet]
        public HttpResponseMessage GetMail(string name)
        {
            using (GlobalDesignEntities entites = new GlobalDesignEntities())
            {
                var mail = entites.RestaurantAccounts.Where(s => s.RestaurantName == name).Select(s => s.RestaurantMail).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, mail);
            }
        }
        [Route("api/Restaurant/GetReservation")]
        [HttpGet]
        public IEnumerable<CustomerOldBooking> GetReservation(string restmail)
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                return entities.CustomerOldBooking.Where(mail => mail.RestaurantMail == restmail).ToList();
            }
        }

        [Route("api/Restaurant/GetProcess")]
        [HttpGet]
        public IEnumerable<RestaurantProcess> GetProcess(string mail)
        {

            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                return entities.RestaurantProcess.Where(s => s.RestaurantMail == mail).ToList();
            }
        }

        [Route("api/Restaurant/UpdateProcess")]
        [HttpPut]
        public HttpResponseMessage UpdateProcess(int id, [FromBody]RestaurantProcess process)
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                try
                {
                    var entity = entities.RestaurantProcess.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tables not found");
                    }
                    else
                    {

                        entity.ProcessName = process.ProcessName;
                        entity.ProcessDate = process.ProcessDate;
                        entity.ProcessStatus = process.ProcessStatus;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
        [Route("api/Restaurant/DeleteProcess")]
        [HttpDelete]
        public HttpResponseMessage DeleteProcess(int id)
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                var entity = entities.RestaurantProcess.FirstOrDefault(e => e.Id == id);
                entities.RestaurantProcess.Remove(entity);
                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [Route("api/Restaurant/cusOrderDelete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(string mail,string tablename,string itemname,int itemprice)
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                var item = entities.CustomerService.FirstOrDefault(s => s.RestaurantMail == mail && s.TableName == tablename && s.ItemName == itemname && s.ItemPrice == itemprice);
                entities.CustomerService.Remove(item);
                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [Route("api/restaurant/tables/empty")]
        [HttpGet]
        public IEnumerable<RestaurantTable> listOfTables(string restaurantName)
        {

            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                var name = entities.RestaurantAccounts.Where(s => s.RestaurantName == restaurantName).FirstOrDefault();
                return entities.RestaurantTable.Where(s => s.RestaurantMail == name.RestaurantMail).ToList();
            }
        }


        [Route("api/Restaurant/GetTables")]
        [HttpGet]
        public IEnumerable<RestaurantTable> GetTables(string mail)
        {

            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                return entities.RestaurantTable.Where(s => s.RestaurantMail == mail).ToList();

            }
        }

        [Route("api/Restaurant/DeleteCategory")]
        [HttpDelete]
        public HttpResponseMessage DeleteCategory(int id)
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                var entity = entities.RestaurantCategories.FirstOrDefault(e => e.Id == id);
                entities.RestaurantCategories.Remove(entity);
                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [Route("api/Restaurant/UpdateCategory")]
        [HttpPut]
        public HttpResponseMessage UpdateCategory(int id, [FromBody]RestaurantCategories categories)
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                try
                {
                    var entity = entities.RestaurantCategories.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tables not found");
                    }
                    else
                    {

                        entity.CategoryName = categories.CategoryName;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }


        [Route("api/Restaurant/DeleteTables")]
        [HttpDelete]
        public HttpResponseMessage DeleteTable(int id)
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                var entity = entities.RestaurantTable.FirstOrDefault(e => e.Id == id);
                entities.RestaurantTable.Remove(entity);
                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }


        [Route("api/Restaurant/UpdateTables")]
        [HttpPut]
        public HttpResponseMessage UpdateTable(int id, [FromBody]RestaurantTable tables)
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                try
                {
                    var entity = entities.RestaurantTable.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tables not found");
                    }
                    else
                    {

                        entity.TableName = tables.TableName;
                        entity.TableSeatNumber = tables.TableSeatNumber;
                        entity.TableBooking = tables.TableBooking;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }

        [Route("api/Restaurant/UpdateCustomerTable")]
        [HttpPut]
        public HttpResponseMessage UpdateCusTable(string tablename, RestaurantTable tables)
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                try
                {
                    var entity = entities.RestaurantTable.FirstOrDefault(e => e.TableName == tablename);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tables not found");
                    }
                    else
                    {
                        entity.TableBooking = tables.TableBooking;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }

            }
        }

        [Route("api/Restaurant/GetItems")]
        [HttpGet]
        public IEnumerable<RestaurantItems> GetItems(string mail)
        {

            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                return entities.RestaurantItems.Where(s => s.RestaurantMail == mail).ToList();
            }

        }


        [Route("api/Restaurant/GetItemsSearch")]
        [HttpGet]
        public IEnumerable<RestaurantItems> GetEmre(string restaurantName)
        {

            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                var name = entities.RestaurantAccounts.Where(s => s.RestaurantName == restaurantName).FirstOrDefault();
                return entities.RestaurantItems.Where(s => s.RestaurantMail == name.RestaurantMail).ToList();
            }

        }
        [Route("api/Restaurant/TimeZone")]
        [HttpGet]
        public HttpResponseMessage GetTimeZone(string tablename)
        {

            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                try
                {
                    var ne = entities.CustomerOldBooking.Where(s => s.BookingMessage == tablename).Select(s => s.BookingDate).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, ne);
                }

                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
            
        }

        [Route("api/Restaurant/GetService")]
        [HttpGet]
        public IEnumerable<RestaurantItems> GetMesut(string mail, string cat)
        {

            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                return entities.RestaurantItems.Where(s => s.RestaurantMail == mail && s.ItemCategory == cat).ToList();
            }
        }
        [Route("api/Restaurant/GetOrder")]
        [HttpGet]
        public IEnumerable<CustomerService> GetCustomerOrder(string mail,string tablename)
        {
            using(GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                return entities.CustomerService.Where(s => s.RestaurantMail == mail && s.TableName == tablename).ToList();
            }
        }

        [Route("api/Restaurant/GetCustomerItem")]
        [HttpGet]
        public IEnumerable<RestaurantItems> GetCustomerItem(string mail, string name)
        {

            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                return entities.RestaurantItems.Where(s => s.RestaurantMail == mail && s.ItemName == name).ToList();
            }


        }
        [Route("api/Restaurant/DeleteItems")]
        [HttpDelete]
        public HttpResponseMessage DeleteItems(int id)
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                var entity = entities.RestaurantItems.FirstOrDefault(e => e.Id == id);
                entities.RestaurantItems.Remove(entity);
                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [Route("api/Restaurant/UpdateItems")]
        [HttpPut]
        public HttpResponseMessage UpdateItems(int id, [FromBody]RestaurantItems items)
        {
            using (GlobalDesignEntities entities = new GlobalDesignEntities())
            {
                try
                {
                    var entity = entities.RestaurantItems.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tables not found");
                    }
                    else
                    {

                        entity.ItemCategory = items.ItemCategory;
                        entity.ItemName = items.ItemName;
                        entity.ItemPrice = items.ItemPrice;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }


            }
        }
    }
}
