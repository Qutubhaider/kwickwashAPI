//using APIKwickWash.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIKwickWash.Controllers
{
    public class DriverCounter
    {
        public string NewOrder;
        public string ProcessOrder;
        public string DeliveredOrder;
        public string TotalOrder;
    }
    public class DriverCounterController : ApiController
    {
        public List<DriverCounter> GetCouter(string driverId)
        {
            List<DriverCounter> DriverCounter = new List<DriverCounter>();
            DriverCounter data = new DriverCounter();
            int TotalCounter = 0;
            string query = "select count(*)NewOrder from tbl.kwickOrder where did='" + driverId.ToString() + "'  " +
                " select count(*)as ProcessOrder from tbl.orders where did='" + driverId.ToString() + "' and deliveryStatus='' " +
                " select count(*)as DeliveredOrder from tbl.orders where did='" + driverId.ToString() + "' and deliveryStatus='Delivered'";
            DataSet ds = Database.get_DataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                data.NewOrder = ds.Tables[0].Rows[0]["NewOrder"].ToString();
                TotalCounter += Convert.ToInt32(ds.Tables[0].Rows[0]["NewOrder"]);
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                data.ProcessOrder = ds.Tables[1].Rows[0]["ProcessOrder"].ToString();
                TotalCounter += Convert.ToInt32(ds.Tables[1].Rows[0]["ProcessOrder"]);
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                data.DeliveredOrder = ds.Tables[2].Rows[0]["DeliveredOrder"].ToString();
                TotalCounter += Convert.ToInt32(ds.Tables[2].Rows[0]["DeliveredOrder"]);
            }
            data.TotalOrder = TotalCounter.ToString();
            DriverCounter.Add(data);
            return DriverCounter;
        }

    }
}
