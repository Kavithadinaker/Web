using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;


namespace WebApplication.Controllers
{
    public class LoginController : ApiController
    {

        /// <summary>
        /// Parameters username and password are obtained from database 
        /// Sqlquery is use to check whether the details are correct or not.
        /// Based on the value obtained from sqldata reader the condition is checked and the message is 
        /// returned as a json type.
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        // GET: UserLogin      
       
        public HttpResponseMessage LoginDetails(string UserName, string Password)
        {
            string message = string.Empty;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["LoginConnectionString"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                string sqlquery = "Select USERNAME,PWD from USERTABLE where USERNAME=@USERNAME and PWD=@PWD ";
                sqlconn.Open();
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlcomm.Parameters.AddWithValue("@USERNAME", UserName);
                sqlcomm.Parameters.AddWithValue("@PWD", Password);
                SqlDataReader sdr = sqlcomm.ExecuteReader();

                if (sdr.Read())
                {
                    message = "Login SuccessFul";
                }
                else
                {
                    message = "Login Failed";
                    //log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
                    //logger.Error("failed");

                }
                sqlconn.Close();
            }
            catch (Exception ex)
            {

                message = "Login Failed";
                //log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
                //logger.Error("Meesage:" + ex.Message + "\r\n stacktrace" + ex.StackTrace);
                throw (ex);
            }

            return Request.CreateResponse(HttpStatusCode.Created, message); 

        }
    }
}