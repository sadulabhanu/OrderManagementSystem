using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;

namespace OrderManagementSystem.Controllers
{
    public class AdminController : ApiController
    {
        //Retrieves all the order details
        //GET: api/admin
        public HttpResponseMessage GetOrders()
        {
            using(OrderManagementSystemEntities entities=new OrderManagementSystemEntities())
            {
                var entity = entities.Orders.ToList();
                if (entity != null)
                    return Request.CreateResponse(HttpStatusCode.OK, entities.sp_getallOrders().ToList());
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Orders as of now.");
            }
        }
    }
}
