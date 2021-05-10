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
    public class franchiseController : ApiController
    {
        public IEnumerable<franchise> Get()
        {
            string query = "select * from tbl.franchise";
            DataTable dt = Database.get_DataTable(query);
            List<franchise> franchise = new List<Models.franchise>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    franchise.Add(new ReadFranchise(dr));
                }
            }
            return franchise;
        }

        public string Post([FromBody] CreateFranchise values)
        {
            try
            {
                string query_franchise = "";
                if (values.franchiseId == "0")
                {
                    query_franchise = "insert into tbl.franchise (name,mobile,email,city) values ('" + values.name
                        + "','" + values.mobile + "','" + values.email + "','" + values.city + "')";
                }
                else
                {
                    query_franchise = "update tbl.franchise set name='" + values.name + "', mobile='" + values.mobile
                        + "', email='" + values.email + "',city='" + values.city + "' where franchiseId='" + values.franchiseId + "'";
                }
                int res = Database.Execute(query_franchise);
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
