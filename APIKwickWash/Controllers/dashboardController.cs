using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using APIKwickWash.Models;
using System.Web.Cors;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace APIKwickWash.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class dashboardController : ApiController
    {
       
        public IEnumerable<dashboard> Get()
        {
            string query = "select * from tbl.ttlUserDashboard";
            DataTable dt = Database.get_DataTable(query);
            List<dashboard> dashboard = new List<Models.dashboard>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dashboard.Add(new ReadDashboard(dr));
                }
            }
            return dashboard;
        }

        public IEnumerable<dashboard> Get(int id)
        {
            string query = "select * from tbl.ttlUserDashboard where userid='" + id + "'";
            DataTable dt = Database.get_DataTable(query);
            List<dashboard> dashboard = new List<Models.dashboard>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dashboard.Add(new ReadDashboard(dr));
                }
            }
            return dashboard;
        }
    }
}
