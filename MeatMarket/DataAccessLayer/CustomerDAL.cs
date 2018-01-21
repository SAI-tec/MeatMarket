using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DataAccessLayer
{
    public class CustomerDAL
    {

        static string _connectionString = ConfigurationManager.ConnectionStrings["MeatMarketConnection"].ConnectionString;
        public static string ConnectionString { get { return _connectionString; } }
        public List<Customer> GetAll()
        {
            List<Customer> lc = new List<Customer>();
           // lc.Add(new Customer() { CustomerID = 2, CustomerUsername = "Divya", CustomerPassword = "divya1901", CustomerEmailAddress = "divya1901@gmail.com" });
            //Establish SQL connection
            using (SqlConnection sc = new SqlConnection(ConnectionString))
            {
                //open connection
                sc.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Customer", sc);

                da.SelectCommand.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                da.Fill(dt);


                foreach (DataRow row in dt.Rows)
                {
                    lc.Add(new Customer { CustomerID = (int)row["CustomerID"], CustomerUsername = row["CustomerUsername"].ToString(), CustomerPassword = row["CustomerPassword"].ToString(), CustomerEmailAddress = row["CustomerEmailAddress"].ToString() });
                }

                return lc;
            }

        }
    }

    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerUsername { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerEmailAddress { get; set; }
    }


    
}
