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
    public class myserviceController : ApiController
    {
        // GET: myservice
        public IEnumerable<kwickService> Get(int id)
        {
            string query = "select * from tbl.myservice ms join tbl.service s on ms.srid=s.srid where ms.userid='" + id + "' order by s.orderno asc";
            DataTable dt = Database.get_DataTable(query);
            List<kwickService> services = new List<Models.kwickService>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    services.Add(new ReadService(dr));
                }
            }
            return services;
        }
    }
}