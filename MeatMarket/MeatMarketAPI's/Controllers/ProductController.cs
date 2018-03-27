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
    public class ProductController : ApiController
    {
        [Route("GetAll")]
        [HttpGet]
        public Response<List<Product>> GetAll()
        {
            ProductDAL pro = new ProductDAL();
            return pro.GetAll() as Response<List<Product>>;
        }

        [Route("GetById/{ProductId: int}")]
        [HttpGet]
        public Response<Product> GetById(int ProductId)
        {
            ProductDAL pro = new ProductDAL();
            return pro.GetById(ProductId) as Response<Product>;
        }

        [Route("Create/{Product : string}")]
        [HttpGet]
        public Response<Product> Create(Product Product)
        {
            ProductDAL pro = new ProductDAL();
            return pro.Create(Product) as Response<Product>;
        }

        [Route("Remove/{ProductId : int}")]
        [HttpDelete]
        public Response<Product> Remove(int ProductId)
        {
            ProductDAL pro = new ProductDAL();
            return pro.Remove(ProductId) as Response<Product>;
        }


        [Route("Update/{Product : string}")]
        [HttpPost]
        public Response<Product> Update(Product Product)
        {
            ProductDAL pro = new ProductDAL();
            return pro.Update(Product) as Response<Product>;
        }
    }
}
