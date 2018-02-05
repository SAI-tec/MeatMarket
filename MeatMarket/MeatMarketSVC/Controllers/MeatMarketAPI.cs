using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using System.Web.Http.Controllers;

namespace MeatMarket
{
    public class BaseController : ApiController {
    }

    public class MyCustomFilter : ActionFilterAttribute {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            Debug.WriteLine("");
            base.OnActionExecuted(actionExecutedContext);
        }
    }

    public class CustomerModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            // implement modelbinder
            return true;
        }
    }

    [MyCustomFilter]
   // [RoutePrefix("MeatMarket")]
    public class MeatMarketAPI : BaseController
    {
        // GET api/<controller>
        [Route("GetAll")]
        public IEnumerable<string> GetAll()
        {
            
            try
            {
                throw new Exception("thorwn from dal");
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [Route("Get")]
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put([FromUri]int id, [FromBody] [ModelBinder(typeof(CustomerModelBinder))]Customer value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
    
    public class Customer {
        public Type MyProperty { get; set; }
    }
}