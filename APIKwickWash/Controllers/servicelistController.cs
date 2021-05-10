using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIKwickWash.Models;

namespace APIKwickWash.Controllers
{
    public class servicelistController : ApiController
    {
        // GET: servicelist
        public IEnumerable<kwickService> Get(int id)
        {          
            
            string query = "select * from tbl.service";
            DataTable dt = Database.get_DataTable(query);
            List<kwickService> profile = new List<Models.kwickService>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string queryMyser = "select * from tbl.myservice where userid='" + id + "' and serviceName='" + dr["serviceName"] + "'";
                    DataSet ds = Database.get_DataSet(queryMyser);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        profile.Add(new ReadService(dr));
                    }
                }
            }
            return profile;
        }

       
    }
}