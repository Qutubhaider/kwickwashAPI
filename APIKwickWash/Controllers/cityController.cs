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
    public class cityController : ApiController
    {
        public IEnumerable<city> Get()
        {
            string query = "select * from tbl.city order by cityName asc";

            DataTable dt = Database.get_DataTable(query);
            List<city> city = new List<Models.city>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    city.Add(new ReadCity(dr));
                }
            }
            return city;
        }

        public IEnumerable<city> Get(int id)
        {
            string query = "select * from tbl.city where cityId='" + id + "' order by cityName asc";
            DataTable dt = Database.get_DataTable(query);
            List<city> city = new List<Models.city>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    city.Add(new ReadCity(dr));
                }
            }
            return city;
        }

        public string Post([FromBody]CreateCity values)
        {
            try
            {
                string query_city = "";
                if(values.cityId=="0")
                {
                    query_city = "insert into tbl.city(cityName) values ('" + values.cityName + "')";
                }
                else
                {
                    query_city = "update tbl.city set cityName='" + values.cityName + "' where cityId='" + values.cityId + "'";
                }                
                int res = Database.Execute(query_city);
                if(res==1)
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
