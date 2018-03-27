using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static MeatMarketAPI_s.Attributes;

namespace MeatMarketAPI_s.Controllers
{
    [CustomFilter]
    [RoutePrefix("api/ProductTest")]
    public class OrderController : ApiController
    {
        [Route("GetAll")]
        [HttpGet]
        public Response<List<Order>> GetAll()
        {
            OrdersDAL ord = new OrdersDAL();
            return ord.GetAll() as Response<List<Order>>;
        }

        [Route("GetById/{OrderID: int}")]
        [HttpGet]
        public Response<Order> GetById(int OrderID)
        {
            OrdersDAL ord = new OrdersDAL();
            return ord.GetById(OrderID) as Response<Order>;
        }

        [Route("Create/{Orders : string}")]
        [HttpGet]
        public Response<Order> Create(Order Orders)
        {
            OrdersDAL ord = new OrdersDAL();
            return ord.Create(Orders) as Response<Order>;
        }

        [Route("Remove/{ OrderID : int}")]
        [HttpDelete]
        public Response<Order> Remove(int OrderID)
        {
            OrdersDAL ord = new OrdersDAL();
            return ord.Remove(OrderID) as Response<Order>;
        }


        [Route("Update/{Orders : string}")]
        [HttpPost]
        public Response<Order> Update(Order Orders)
        {
            OrdersDAL ord = new OrdersDAL();
            return ord.Update(Orders) as Response<Order>;
        }
    }
}
