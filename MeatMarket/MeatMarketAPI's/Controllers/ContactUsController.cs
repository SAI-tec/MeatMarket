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
    [RoutePrefix("api/ContactUstest")]
    public class ContactUsController : ApiController
    {
        private IContactUsDAL _con;
        public ContactUsController(IContactUsDAL con) // new ContactUsDAL()
        {
            _con = con;
        }

        [HttpGet]
        [Route("GetAll")]
        public Response<List<Contacts>> GetAll()
        {
            return _con.GetAll() as Response<List<Contacts>>;
        }

        [HttpGet]
        [Route("Create/{Contact : string}")]
        public Response<List<Contacts>> Create(Contacts Contact)
        {
            if (ModelState.IsValid)
            {
                return _con.Create(Contact) as Response<List<Contacts>>;

                // 200 HTTP --> successgul
            }
            else {
                // 400 bad 
              return  new Response<List<Contacts>>()
                {
                    Errors = ModelState.Values.SelectMany(p => p.Errors).Select(p => p.ErrorMessage).ToArray()
                };
            }
        }

        [HttpPost]
        [Route("Update")]
        public Response<Contacts> Update(Contacts Contact)
        {
            ContactUsDAL con = new ContactUsDAL();
            return con.Update(Contact) as Response<Contacts>;

        }

        [HttpGet]
        [Route(" GetById/{ID : int}")]
        public Response<Contacts> GetById(int ID)
        {
            ContactUsDAL con = new ContactUsDAL();
            return con.GetById(ID) as Response<Contacts>;
        }


    }
}
