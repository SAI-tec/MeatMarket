using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
   public class OrdersDAL : BaseDAL<Order>
    {

        public override IResponse GetAll()
        {
            List<Order> OrdersList = new List<Order>();

            // Using Delegate for SQL Connection
            return Connect( (conn) =>
            {

                // Connection Open
                conn.Open();
                SqlDataAdapter DA = new SqlDataAdapter("SELECT * FROM [dbo].[Orders] ", conn);

                DA.SelectCommand.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                DA.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    OrdersList.Add(new Order
                    {
                        OrderID         = (int)row["OrderID"],
                        OrderQuantity   = (int)row["OrderQuantity"],
                        OrderPrice      = (decimal)row["OrderPrice"],
                        OrderDate       = (DateTime)row["OrderDate"],
                        AddressId       = (int) row["AddressId"],
                        ProductId       = (int) row["ProductId"]
                    });
                }

                return new Response<List<Order>>() { Data = OrdersList as List<Order> };
            });
        }

        public override IResponse GetById(int OrderID)
        {
            return Connect((conn) => {
                conn.Open();
                using (SqlCommand Command = new SqlCommand("SELECT * FROM [dbo].[Orders]"))
                {
                    Command.CommandType = CommandType.Text;
                    SqlDataReader DataRdr = Command.ExecuteReader();

                    if (DataRdr.Read())
                        return new Response<Order>()
                        {
                            Data = new Order()
                            {
                                OrderID       = (int)DataRdr["OrderID"],
                                OrderQuantity = (int)DataRdr["OrderQuantity"],
                                OrderPrice    = (decimal) DataRdr["CustomerPassword"],
                                OrderDate     = (DateTime)DataRdr["OrderDate"],
                                AddressId     = (int) DataRdr["AddressId"],
                                ProductId     = (int) DataRdr ["ProductId"]
                            } as Order
                        };
                    else return null;
                }
            });
        }

        public override IResponse Update(Order Orders)
        {
           return base.Connect((conn) =>
            {
                conn.Open();

                SqlCommand Command = new SqlCommand(
                $"Update [dbo].[Orders] set  OrderID = { Orders.OrderID } OrderQuantity = { Orders.OrderQuantity} OrderPrice = { Orders.OrderPrice }   OrderDate = { Orders.OrderDate} AddressId = {Orders.AddressId} ProductId = {Orders.ProductId} where OrderID = @OrderID",
                conn);
                Command.CommandType = CommandType.Text;
                Command.Parameters.AddWithValue("@OrderID", Orders.OrderID);
                Command.ExecuteNonQuery();

                return new Response<Order>();
            });
        }

        public override IResponse Remove(int OrderID)
        {
           return base.Connect((conn) =>
            {
                conn.Open();
                SqlCommand Command = new SqlCommand("Delete From [dbo].[Orders] where OrderID = @OrderID ", conn);
                Command.CommandType = CommandType.Text;
                Command.Parameters.AddWithValue("@OrderID", OrderID);
                Command.ExecuteNonQuery();

                return new Response<Order>();
            });
        }

        public override IResponse Create(Order Orders)
        {

            return base.Connect((conn) =>
            {
                using (SqlCommand Command = new SqlCommand("$OrderID", conn))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    var Data = Command.ExecuteScalar();
                }
                return new Response<Order>();
            });
        }

        //public IResponse  GetProductAndOrdersByID(int OrderID)
        //{
        //    return Connect(ConnectionString, (conn) =>
        //    {
        //        Order ord = new Order();
        //        //Product pro = new Product();

        //        //Relation  {DataRelation custOrderRel = custDS.Relations.Add("CustOrders",
        //        //    custDS.Tables["Customers"].Columns["CustomerID"],
        //        //             custDS.Tables["Orders"].Columns["CustomerID"]);
        //        // enum , features in 4.0/4.5
        //        //}

        //        // Multiple tables
        //        //SqlDataAdapter cmd1 = new SqlDataAdapter("GetCustomerAndOrdersById", conn);
        //        //cmd1.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        //cmd1.SelectCommand.Parameters.AddWithValue("@OrderID", ord.OrderID);

        //        //DataSet ds = new DataSet();
        //        //cmd1.Fill(ds);

        //        //ds.Relations.Add("Test", ds.Tables["Orders"].Columns["OrderID"],
        //        //                         ds.Tables["Product"].Columns["OrderID"]);
        //        //if (ds.Tables["Orders"].Rows.Count > 0) {

        //        //    ord.OrderID       = (int)ds.Tables["Orders"].Rows[0]["OrderID"];
        //        //    ord.OrderPrice    = (decimal)ds.Tables["Orders"].Rows[0]["OrderPrice"];
        //        //    ord.OrderQuantity = (int)ds.Tables["Orders"].Rows[0]["OrderQuantity"];

        //        //foreach (DataRow item in ds.Tables["Product"].Rows)
        //        //    {

        //        //        ord.ProductId = (int)ds.Tables["Product"].Rows[0]["ProductId"];
        //        //    }
        //        //}
                
        //        // By Using inner join
        //        conn.Open();
        //        SqlDataAdapter cmd = new SqlDataAdapter("SELECT O.OrderID, P.ProductName, P.ProductID, P.ProductPrice from Orders O inner join Product P on O.OrderID = P.OrderID where O.OrderID = 2;", conn);
        //        cmd.SelectCommand.CommandType = CommandType.Text;
        //        cmd.SelectCommand.Parameters.AddWithValue("", ord.);

        //        DataTable dt = new DataTable();
        //        cmd.Fill(dt);

        //        if (dt.Rows.Count > 0)
        //        {
        //            ord.OrderID = (int)dt.Rows[0]["OrderID"];
        //            ord.Product = new List<Product>();

        //            foreach (DataRow item in dt.Rows)
        //            {
        //                ord.Product.Add(new Product { ProductName = item["ProductName"].ToString(), ProductId = (int)item["ProductId"], ProductPrice = item["ProductPrice"].ToString() });
        //            }

        //        }

        //        return new Response<Order>() { Data = ord };
        //    });
        //}

    }

    // one to one

    public class Order
    {
        public int OrderID { get; set; }
        public int OrderQuantity { get; set; }
        public decimal OrderPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public int AddressId { get; set; }
        public int ProductId { get; set; }
       // public Customer Customer { get; set; }
        public List<Product> Product { get; set; }
    }
}
