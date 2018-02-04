using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public abstract class BaseDAL<T> where T : class
    {
        protected IResponse Connect(string connectionString, Func<SqlConnection, IResponse> executeCommand)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    return executeCommand(conn);
                }
            }
            catch (Exception ex)
            {
                //logging  mechanism
                return new ErrorsResponse() { Errors = new string[] { ex.ToString() } };
            }
        }
        public abstract IResponse GetAll();
        public abstract IResponse GetById(int CustomerID);
        public abstract void Create(Customer customer);
        public  abstract void Remove(int Id);
        public abstract void Update(Customer customer);
    }


    public class ErrorsResponse :IResponse{
        public string[] Errors { get; set; }
    }

    public interface IResponse {

    }

    //Create // Update // GetByID
    public class Response<T> :ErrorsResponse where T : class {
        public T Data { get; set; }
    }

    //GetALL
    public class ResponseGetAll<T> : ErrorsResponse where T : class 
    {
        public List<T> Data { get; set; }
    }

    public static class ExtensionMethod {
        public static string ToDivya(this string someOtherstring) {
            return someOtherstring.Replace(".", "");
            //return "Divya"; // bsuiness logic
        }
    }
}
