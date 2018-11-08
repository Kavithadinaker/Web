using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;


namespace WebApplication.Controllers
{
    [EnableCorsAttribute("http://localhost:55325", "*", "*")]
    public class Login1Controller : ApiController
    {

       [HttpPost]
        public string Login(string Username, string Password)
        {
            string message = string.Empty;
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["LoginConnectionString"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                string sqlquery = "Select USERNAME,PWD from USERTABLE where USERNAME=@USERNAME and PWD=@PWD ";
                sqlconn.Open();
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlcomm.Parameters.AddWithValue("@USERNAME", Username);
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
                throw (ex);
                //log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
                //logger.Error("Meesage:" + ex.Message + "\r\n stacktrace" + ex.StackTrace);


            }

            return message;
        }

    }
}
