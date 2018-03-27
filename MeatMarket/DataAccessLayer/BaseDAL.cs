﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public abstract class BaseDAL<T> : IRepository<T> where T : class
    {
        static string _connectionString = ConfigurationManager.ConnectionStrings["MeatMarketConnection"].ConnectionString;
        public static string ConnectionString { get { return _connectionString; } }

        public abstract IResponse Create(T data);

        public abstract IResponse GetAll();

        public abstract IResponse GetById(int data);

        public abstract IResponse Remove(int data);

        public abstract IResponse Update(T data);

        protected IResponse Connect(Func<SqlConnection, IResponse> executeCommand)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
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
        
    }

    public interface IRepository<T> where T:class {
         IResponse GetAll();
        IResponse GetById(int data);
        IResponse Create(T data);
        IResponse Remove(int data);
        IResponse Update(T data);
    }


    public class ErrorsResponse : IResponse
    {
        public string[] Errors { get; set; }
    }

    public interface IResponse
    {

    }

    //Create // Update // GetByID
    public class Response<T> : ErrorsResponse where T : class
    {
        public T Data { get; set; }
    }

    //GetALL
    public class ResponseGetAll<T> : ErrorsResponse where T : class
    {
        public List<T> Data { get; set; }
    }

    public static class ExtensionMethod
    {
        public static string ToDivya(this string someOtherstring)
        {
            return someOtherstring.Replace(".", "");
            
        }
    }
}

