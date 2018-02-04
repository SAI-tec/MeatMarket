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
    public class CustomerDAL<T>: BaseDAL<T> where T:class
    {

        static string _connectionString = ConfigurationManager.ConnectionStrings["MeatMarketConnection"].ConnectionString;
        public static string ConnectionString { get { return _connectionString; } }
        public override IResponse GetAll()
        {
            List<Customer> lc = new List<Customer>();
            // lc.Add(new Customer() { CustomerID = 2, CustomerUsername = "Divya", CustomerPassword = "divya1901", CustomerEmailAddress = "divya1901@gmail.com" });
            //Establish SQL connection
            return Connect(ConnectionString, (conn) =>
            {
                //open connection
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Customer", conn);

                da.SelectCommand.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    lc.Add(new Customer { CustomerID = (int)row["CustomerID"], CustomerUsername = row["CustomerUsername"].ToString(), CustomerPassword = row["CustomerPassword"].ToString(), CustomerEmailAddress = row["CustomerEmailAddress"].ToString() });
                }

                return new Response<List<T>>() { Data = lc as List<T> };
            });

        }

        public override IResponse GetById(int CustomerID)
        {
           return Connect(ConnectionString, (conn) => {
                //open connection
                conn.Open();
                SqlCommand sc = new SqlCommand("CustomerGetDatabyID");
            using (SqlDataReader rdr = sc.ExecuteReader())
            {
              
                if (rdr.Read() )
                    return new Response<T>()
                    {
                        Data = new Customer()
                        {
                            CustomerID = (int)rdr["CustomerID"],
                            CustomerUsername = rdr["CustomerUserName"].ToString(),
                            CustomerPassword = rdr["CustomerPassword"].ToString(),
                            CustomerEmailAddress = rdr["CustomerEmailAddress"].ToString(),

                        } as T
                    };
                else return null;
                
            }
            });


        }

        public override void Create(Customer customer)
        {
            Connect(ConnectionString, (conn) =>
            {
                //open connection
                conn.Open();

                //using (SqlCommand comd = new SqlCommand($"Exec CreateCustomerId @CustomerUseName = { customer.CustomerUsername }, @CustomerPassword = { customer.CustomerPassword }, @customerEmailAddress = {customer.CustomerEmailAddress}", conn))
                using (SqlCommand comd = new SqlCommand($"CreateCustomerId", conn))
                {
                    //comd.Parameters.AddWithValue()@CustomerUseName = { customer.CustomerUsername }, @CustomerPassword = { customer.CustomerPassword }, @customerEmailAddress = { customer.CustomerEmailAddress}
                    comd.CommandType = CommandType.StoredProcedure;
                    var cr = comd.ExecuteScalar();
                }
                return new Response<T>();

            });
        }

        //    List<Customer> custList = new List<Customer>();
        //    List<Customer> c = new List<Customer>();
        //    List<List<Customer>> cc = new List<List<Customer>>();

        //    foreach (var item in custList)
        //    {
        //        if (item.CustomerID > 4) {
        //            c.Add(item);
        //        }
        //    }
        // var test=   ".Net".ToDivya();

        //  List<Customer> newlst=   custList.Where(p => p.CustomerID > 4).ToList();

        //    int[] arra = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //    arra.Cast<string>();
        //    foreach (var a in arra)
        //    {
        //        if (a > 4)
        //        {

        //        }
        //    }
        //}
        public override void Update(Customer customer)
        {
            base.Connect(ConnectionString, (conn) => {
                //open connection
                conn.Open();
                using (SqlCommand comd = new SqlCommand($"Update customer Set CustomerUseName = { customer.CustomerUsername } CustomerPassword = { customer.CustomerPassword } customerEmailAddress = {customer.CustomerEmailAddress} Where CustomerID = @CustomerID", conn))
                {

                    comd.CommandType = CommandType.Text;
                    var cr = comd.ExecuteNonQuery();
                }

                return new Response<T>();

            });
            
        }


        public override void Remove(int Id)
        {
            base.Connect(ConnectionString, (conn) => {
                //open connection
                conn.Open();
                using (SqlCommand comd = new SqlCommand("Delete from customer Where CustomerID = " + Id, conn))
                {
                    comd.CommandType = CommandType.Text;
                    var cr = comd.ExecuteNonQuery();
                }

                return new Response<T>();

            });

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
