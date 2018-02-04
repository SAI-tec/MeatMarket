using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace ContactConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                ContactUsDAL dal = new ContactUsDAL();
                var t = dal.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
