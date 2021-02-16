using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;

namespace OrderManagementSystem.Controllers
{
    public class OrderController : ApiController
    {
        // GET: api/Order
        public HttpResponseMessage Get()
        {
            using(OrderManagementSystemEntities entitites=new OrderManagementSystemEntities())
            {
                var entity = entitites.Orders.ToList();
                if (entity != null)
                    return Request.CreateResponse(HttpStatusCode.OK, entitites.sp_Orders_CRUD(0, 0, " ", 0, " ").ToList());
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Orders as of now.");

            }
        }

        // GET: api/Order/5
        public HttpResponseMessage Get(int id)
        {
            using (OrderManagementSystemEntities entitites = new OrderManagementSystemEntities())
            {
                var entity = entitites.Orders.FirstOrDefault(o => o.OrderID == id);
                if (entity != null)
                    return Request.CreateResponse(HttpStatusCode.OK, entitites.sp_Orders_CRUD(id, 0,"",0,"GetOrder").ToList());
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Orders with ID: "+id.ToString());

            }
        }

        // POST: api/Order
        public HttpResponseMessage Post([FromBody]Order order)
        {
            try
            {
                using(OrderManagementSystemEntities entities =new OrderManagementSystemEntities())
                {
                    entities.Orders.Add(order);
                    entities.SaveChanges();
                    //Below message return status code 201(Created) and URI(location and order) once created
                    var message = Request.CreateResponse(HttpStatusCode.Created, order);
                    message.Headers.Location = new Uri(Request.RequestUri + order.OrderID.ToString());
                    return message;

                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT: api/Order/5
        public HttpResponseMessage Put([FromUri]int id, [FromBody]Order order)
        {
            try
            {
                using (OrderManagementSystemEntities entities = new OrderManagementSystemEntities())
                {
                    var entity = entities.Orders.FirstOrDefault(o => o.OrderID == id);
                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Order with ID: " + id.ToString() +" not found to update.");
                    }
                    else
                    {
                        entity.Product_ID = order.Product_ID;
                        entity.BuyerID = order.BuyerID;
                        entity.Order_Status = order.Order_Status;
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/Order/5
        public HttpResponseMessage Delete([FromUri]int id)
        {
            try
            {
                using (OrderManagementSystemEntities entities = new OrderManagementSystemEntities())
                {
                    var entity = entities.Orders.FirstOrDefault(o => o.OrderID == id);
                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Order with ID: " + id.ToString() + " not found.");
                    }
                    else
                    {
                        entities.Orders.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
                
        }
    }
}
