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
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace APIKwickWash.Controllers
{
    public class SalesReportController : ApiController
    {
        public IEnumerable<placeorder> Get(int id,int sid,string fdate,string tdate)
        {
            string query = "select oi.orderItemId,oi.OrderId,oi.CUserId,oi.SUserId,oi.InvoiceNo,oi.SrId,oi.ServiceName,oi.proId,oi.ProductName,oi.Unit,oi.price," +
                 " oi.orderQty,oi.totalQty,p.productCode,s.serviceDescription from tbl.OrderItems oi join tbl.product p on oi.proId = p.proId join tbl.service s" +
                 " on oi.SrId = s.SrId join tbl.Orders o  on o.orderId=oi.orderId where oi.suserid='" + id.ToString() + "' and oi.srId='" + sid + "' " +
                 " and convert(date,o.orderDate) between '" + fdate + "' and '" + tdate + "'";
            DataTable dt = Database.get_DataTable(query);
            List<placeorder> placeorder = new List<Models.placeorder>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    placeorder.Add(new Readplaceorder1(dr));
                }
            }
            return placeorder;
        }
    }
}