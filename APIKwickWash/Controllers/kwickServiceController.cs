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
    public class kwickServiceController : ApiController
    {
        // GET: kwickService
        public IEnumerable<kwickService> Get()
        {
            string query = "select * from tbl.service where status='1' order by orderno asc";
            DataTable dt = Database.get_DataTable(query);
            List<kwickService> profile = new List<Models.kwickService>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    profile.Add(new ReadService(dr));
                }
            }
            return profile;
        }

        public IEnumerable<kwickService> Get(int id)
        {
            string query = "select * from tbl.service where srid='" + id + "'";
            DataTable dt = Database.get_DataTable(query);
            List<kwickService> profile = new List<Models.kwickService>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    profile.Add(new ReadService(dr));
                }
            }
            return profile;
        }

        public string Post([FromBody]CreateService values)
        {
            try
            {
                string query_Service = "";
                if (values.sid == 0)
                {
                    query_Service = "insert into tbl.service (serviceName,serviceDescription,serviceImg,StartAt,Duration,Unit) values ('" + values.serviceName
                        + "','" + values.serviceDescription + "','" + values.serviceImg + "','" + values.startAt + "','" + values.duration + "','" + values.Unit + "')";
                }
                else
                {
                    query_Service = "update tbl.service set serviceName='" + values.serviceName + "', serviceDescription='" + values.serviceDescription
                        + "', serviceImg='" + values.serviceImg + "',StartAt='" + values.startAt + "',Duration='" + values.duration
                        + "',Unit='" + values.Unit + "' where srid='" + values.sid + "'";
                }
                int res = Database.Execute(query_Service);
                if (res == 1)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception ex)
            {
                return "false";
            }

        }

        public string Put(int id, [FromBody]CreateService value)
        {
            string query = "update tbl.service set serviceName='" + value.serviceName + "',serviceDescription='" + value.serviceDescription
                + "',serviceImg='" + value.serviceImg + "' where srId='" + id + "'";
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