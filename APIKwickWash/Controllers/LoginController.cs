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
    
    public class LoginController : ApiController
    {
        public IEnumerable<Login> Get()
        {
            string query = "select * from tbl.login";

            DataTable dt = Database.get_DataTable(query);
            List<Login> login = new List<Models.Login>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    login.Add(new ReadLogin(dr));
                }
            }
            return login;
        }
        public IEnumerable<Login> Get(int id)
        {
            string query = "select * from tbl.login where userid='" + id.ToString() + "'";
            DataTable dt = Database.get_DataTable(query);
            List<Login> login = new List<Models.Login>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    login.Add(new ReadLogin(dr));
                }
            }
            return login;
        }

        [HttpPost]
        public string Post([FromBody]CreateLogin vals)
        {
            string roles = "0";
            if(vals.role=="shop")
            {
                roles = "5";
            }
            else if(vals.role=="custom")
            {
                roles = "2";
            }
            else if(vals.role=="driver")
            {
                roles = "10";
            }
            else
            {
                roles = "9";
            }
            string query = "select * from tbl.login where username='" + vals.username + "' and password='" + vals.password + "' and role='" + roles + "'";
            DataSet ds = Database.get_DataSet(query);
            if(ds.Tables[0].Rows.Count>0)
            {
                return ds.Tables[0].Rows[0]["UserId"].ToString();
            }            
            else
            {
                return "0";
            }
        }
        
        public string Delete(int id)
        {
            string query = "delete from tbl.login where userud='" + id + "'";
            int res = Database.Execute(query);
            if (res == 1)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
    }

}