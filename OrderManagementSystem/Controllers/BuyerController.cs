using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;

namespace OrderManagementSystem.Controllers
{
    public class BuyerController : ApiController
    {
        //Retrieves all the orders related to the Buyer
        //GET: api/buyer/1 
        public HttpResponseMessage GetOrderDetails(int id)
        {
            using (OrderManagementSystemEntities entities = new OrderManagementSystemEntities())
            {
                var entity = entities.Buyers.FirstOrDefault(b => b.BuyerID == id);
                if (entity != null)
                    return Request.CreateResponse(HttpStatusCode.OK, entities.sp_getordersforBuyer(id).ToList());
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Buyer with ID: " + id.ToString() + " not found.");
            }
        }
    }
}
