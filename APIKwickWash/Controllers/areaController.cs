using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIKwickWash.Models;

namespace APIKwickWash.Controllers
{
    public class areaController : ApiController
    {
        public IEnumerable<area> Get()
        {
            string query = "select * from tbl.area order by areaName asc";

            DataTable dt = Database.get_DataTable(query);
            List<area> area = new List<Models.area>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    area.Add(new ReadArea(dr));
                }
            }
            return area;
        }

        public IEnumerable<area> Get(int id)
        {
            string query = "select * from tbl.area where cityId='" + id + "' order by areaName asc";
            DataTable dt = Database.get_DataTable(query);
            List<area> area = new List<Models.area>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    area.Add(new ReadArea(dr));
                }
            }
            return area;
        }

        public List<area> GetAreaList(int id, string val,string vals)
        {
            string query = "select * from tbl.area where cityId='" + id + "' and status='0' order by areaName asc";
            DataTable dt = Database.get_DataTable(query);
            List<area> area = new List<Models.area>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    area.Add(new ReadArea(dr));
                }
            }
            return area;
        }


        public List<area> getShopId(int id, string val)
        {
            string query = "select * from tbl.area where areaId='" + id + "'";
            DataTable dt = Database.get_DataTable(query);
            List<area> area = new List<Models.area>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    area.Add(new ReadArea(dr));
                }
            }
            return area;
        }

        public string Post([FromBody]CreateArea values)
        {
            try
            {
                string query_area = "";
                if (values.areaId == "0")
                {
                    query_area = "insert into tbl.area(areaName,cityId,cityName) values ('" + values.areaName + "','" + values.cityId
                        + "','" + values.cityName + "')";
                }
                else
                {
                    query_area = "update tbl.area set areaName='" + values.areaName + "',cityId='" + values.cityId
                        + "' cityName='" + values.cityName + "' where areaId='" + values.areaId + "'";
                }
                int res = Database.Execute(query_area);
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
                return "0";
            }
        }
    }
}
