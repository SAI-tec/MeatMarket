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
    public class ContactUsDAL : BaseDAL<Contacts> 
    {
        
        public override IResponse GetAll()
        {
            List<Contacts> ContactsList = new List<Contacts>();

            // Using Delegate for SQL Connection
            return Connect((conn) =>
            {

                // Connection Open
                conn.Open();
                SqlDataAdapter DA = new SqlDataAdapter("SELECT * FROM [dbo].[ContactUs] ", conn);

                DA.SelectCommand.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                DA.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    ContactsList.Add(new Contacts
                    {
                        Id = (int)row["Id"],
                        ContactName = row["ContactName"].ToString(),
                        PhoneNumber = row["PhoneNumber"].ToString(),
                        EmailAddress = row["EmailAddress"].ToString(),
                        Comment = row["Comment"].ToString()
                    });
                }

                return new Response<List<Contacts>>() { Data = ContactsList as List<Contacts> };
            });
        }

        public override IResponse Create(Contacts Contact)
        {
           return  base.Connect((conn) =>
            {
                using (SqlCommand Command = new SqlCommand("$CreateId", conn))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    var ces = Command.ExecuteScalar();
                }
                return new Response<Contacts>();
            });
        }
        
        public override void Remove(int Id)
        {
            base.Connect( (conn) =>
            {
                conn.Open();
                SqlCommand Command = new SqlCommand("Delete From [dbo].[ContactUs] where Id = @Id ", conn);
                Command.CommandType = CommandType.Text;
                Command.Parameters.AddWithValue("@Id", Id);
                Command.ExecuteNonQuery();

                return new Response<Contacts>();
            });
        }
       
        public override void Update(Contacts Contact)
        {
            base.Connect((conn) =>
            {
                conn.Open();

                SqlCommand Command = new SqlCommand(
                $"Update [dbo].[ContactUs] set  ID = { Contact.Id } ContactName = { Contact.ContactName} PhoneNumber = { Contact.PhoneNumber }   EmailAddress = { Contact.EmailAddress} Comment = {Contact.Comment} where ContactID = @ContactID",
                conn);
                Command.CommandType = CommandType.Text;
                Command.Parameters.AddWithValue("@ContactID", Contact.Id);
                Command.ExecuteNonQuery();

                return new Response<Contacts>();
            });
        }
  
        public override IResponse GetById(int CustomerID)
        {
            return Connect((conn) => {
                conn.Open();
                using (SqlCommand Command = new SqlCommand("SELECT * FROM [dbo].[ContactUs]"))
                {
                    SqlDataReader DataRdr = Command.ExecuteReader();

                    if (DataRdr.Read())
                        return new Response<Contacts>()
                        {
                            Data = new Contacts()
                            {
                                Id = (int)DataRdr["CustomerID"],
                                ContactName = DataRdr["ContactName"].ToString(),
                                PhoneNumber = DataRdr["CustomerPassword"].ToString(),
                                EmailAddress = DataRdr["CustomerEmailAddress"].ToString(),
                                Comment = DataRdr["Comment"].ToString(),
                            } as Contacts
                        };
                    else return null;
                }
            });
        }

    }


    public class Contacts
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Comment { get; set; }

    }
}




