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
    public class myserviceAppController : ApiController
    {
        // GET: myserviceApp
        [System.Web.Mvc.HttpGet]
        public IEnumerable<kwickService> Get(int id)
        {
            string query = "select ms.srId,s.serviceName,s.serviceDescription,s.serviceImg,ms.status,s.startAt,s.duration,s.Unit from tbl.myservice" +
                "  ms join tbl.service s on s.srid=ms.srid where ms.userid='" + id + "' and ms.srid!=22 and ms.status='1' order by s.orderno asc";
            //string query = "select ms.srId,s.serviceName,s.serviceDescription,s.serviceImg,ms.status,ms.startAt,ms.duration,ms.Unit from " +
            //    "tbl.myservice ms join tbl.service s on s.srid=ms.srid where ms.userid='" + id + "'";
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