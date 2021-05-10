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

namespace APIKwickWash.Controllers
{
    public class SignupController : ApiController
    {
        // GET: Signup
        [HttpPost]
        public string Post([FromBody]CreateLogin values)
        {
            try
            {
                string query_login = "insert into tbl.login (username,password,name,email,role,status,phone) values ('" + values.mobile + "','" + values.password.ToString()
                           + "','" + values.name + "','" + values.email + "','5','1','" + values.mobile + "')";
                int res = Database.Execute(query_login);
                if (res == 1)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }
    }
}