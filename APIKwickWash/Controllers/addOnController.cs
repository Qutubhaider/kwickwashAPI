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
    public class addOnController : ApiController
    {
        public IEnumerable<addOn> Get(int id)
        {
            string query = "select * from tbl.OrderAddOn where orderId='" + id.ToString() + "'";
            DataTable dt = Database.get_DataTable(query);
            List<addOn> addOn = new List<Models.addOn>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    addOn.Add(new ReadaddOn(dr));
                }
            }
            return addOn;
        }

        public string Post([FromBody] CreateaddOn data)
        {
            try
            {
                string queryAddon = "insert into tbl.OrderAddOn (OrderId,ProdctName,Qty,Price,TotalPrice) values ('" + data.OrderId
                    + "','" + data.ProdctName + "','" + data.Qty + "','" + data.Price + "','" + data.TotalPrice + "')";
                int res = Database.Execute(queryAddon);
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
