using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
   public  class ProductDAL : BaseDAL<Product>
    {
       

        public override IResponse GetAll()
        {
            List<Product> ProductList = new List<Product>();

            // Using Delegate for SQL Connection
            return Connect((conn) =>
            {
                // Connection Open
                conn.Open();
                SqlDataAdapter DA = new SqlDataAdapter("SELECT * FROM [dbo].[Product] ", conn);

                DA.SelectCommand.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                DA.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    ProductList.Add(new Product
                    {
                        ProductId            = (int)row["ProductId"],
                        ProductName          = row["ProductName"].ToString(),
                        ProductPrice         = row["ProductPrice"].ToString(),
                        Productstock         = row["Productstock"].ToString(),
                        StockAvailabiity     = row["StockAvailabiity"].ToString(),
                      //  OrderId              = (int)row ["OrderID"]
                    });
                }

                return new Response<List<Product>>() { Data = ProductList as List<Product> };
            });
        }

        public override IResponse GetById(int ProductId )
        {
            return Connect((conn) => {
                conn.Open();
                using (SqlCommand Command = new SqlCommand("SELECT * FROM [dbo].[Product]"))
                {
                    Command.CommandType = CommandType.Text;
                    SqlDataReader DataRdr = Command.ExecuteReader();

                    if (DataRdr.Read())
                        return new Response<Product>()
                        {
                            Data = new Product()
                            {
                                ProductId = (int)DataRdr["ProductId"],
                                ProductName = DataRdr["ProductName"].ToString(),
                                ProductPrice = DataRdr["ProductPrice"].ToString(),
                                Productstock = DataRdr["Productstock"].ToString(),
                                StockAvailabiity = DataRdr["StockAvailabiity"].ToString(),
//                                OrderId = (int)DataRdr["OrderId"],
                            } as Product
                        };
                    else return null;
                }
            });
        }

        public override IResponse Create(Product Product)
        {

           return base.Connect((conn) =>
            {
                using (SqlCommand Command = new SqlCommand("$CreateId", conn))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    var Data = Command.ExecuteScalar();
                }
                return new Response<Product>();
            });
        }

        public override void Remove(int ProductId)
        {
            base.Connect((conn) =>
            {
                conn.Open();
                SqlCommand Command = new SqlCommand("Delete From [dbo].[Product] where ProductId = @ProductId ", conn);
                Command.CommandType = CommandType.Text;
                Command.Parameters.AddWithValue("@ProductId", ProductId);
                Command.ExecuteNonQuery();

                return new Response<Product>();
            });
        }

        public override void Update(Product Product)
        {

            base.Connect((conn) =>
            {
                conn.Open();

                SqlCommand Command = new SqlCommand(
                $"Update [dbo].[Product] set  ProductId = { Product.ProductId } ProductName = { Product.ProductName} ProductPrice = { Product.ProductPrice }   Productstock = { Product.Productstock} StockAvailabiity = {Product.StockAvailabiity} OrderId = {Product.ProductId} where ContactID = @ContactID",
                conn);
                Command.CommandType = CommandType.Text;
                Command.Parameters.AddWithValue("@ProductId", Product.ProductId);
                Command.ExecuteNonQuery();

                return new Response<Product>();
            });
        }


    }

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string Productstock { get; set; }
        public string StockAvailabiity { get; set; }
      //  public List<Location> Locations { get; set; }
    }
    //select * form Products innerJoin ProductLocationsBridge p on p.ProductId = Products.ProductID where P.ProductId =1; 
    public class Locations {
        public List<Product> Products{ get; set; }
    }
}
