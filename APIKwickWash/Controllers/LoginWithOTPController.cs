using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIKwickWash.Models;
using System.Web.Cors;
using System.Web.Http.Cors;
using System.IO;

namespace APIKwickWash.Controllers
{
    public class LoginWithOTPController : ApiController
    {
        public string Post([FromBody] CreateLoginWithOTP val)
        {
            string query_otp = "select * from tblOTPByUser where otp='" + val.otp + "' and userid='" + val.userid + "'";
            DataSet ds = Database.get_DataSet(query_otp);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return val.userid.ToString();
            }
            else
            {
                return "0";
            }
        }
    }
}