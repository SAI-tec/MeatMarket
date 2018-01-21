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
            try
            {
                CustomerDAL dal = new CustomerDAL();
                var t = dal.GetAll();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }
    }
}
