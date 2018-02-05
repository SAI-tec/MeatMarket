using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace MeatMarketSVC
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //services 
            //asp.net routing --> url --> Customer/Get/1
            // routing module
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
