using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductTest();
            OrderTest();
            ContactUsTest();
        }
        public static void ProductTest()
        {
            try
            {
                ProductDAL dal = new ProductDAL();
                var t = dal.GetAll();
            }
            catch (Exception ex)
            {
                //   throw ex;
                Console.WriteLine(ex.ToString());
            }

        }

        public static void OrderTest() {
            try
            {
                OrdersDAL dal = new OrdersDAL();
                var t = dal.GetById(1);
            }
            catch (Exception ex)
            {
                //   throw ex;
                Console.WriteLine(ex.ToString());
            }
        }

        public static void ContactUsTest() {

            try
            {
                ContactUsDAL dal = new ContactUsDAL();
                var t = dal.GetAll();
            }
            catch (Exception ex)
            { //   throw ex;
                Console.WriteLine(ex.ToString());
            }
        }


                
            
        }
    }
