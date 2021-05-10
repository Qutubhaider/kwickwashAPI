using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace APIKwickWash.Models
{
    public class cart
    {
        public string cartId { get; set; }
        public string CUserid { get; set; }
        public string SUserid { get; set; }
        public string srId { get; set; }
        public string serviceName { get; set; }
        public string proId { get; set; }
        public string productName { get; set; }
        public string unit { get; set; }
        public string price { get; set; }
        public string orderQty { get; set; }
        public string totalQty { get; set; }
        public string proImg { get; set; }
        public string pricetype { get; set; }
    }

    public class CreateCart:cart
    {

    }

    public class ReadCart:cart
    {
        public ReadCart(DataRow dr)
        {
            cartId = dr["cartId"].ToString();
            CUserid = dr["CUserid"].ToString();
            SUserid = dr["SUserid"].ToString();
            srId = dr["srId"].ToString();
            serviceName = dr["serviceName"].ToString();
            proId = dr["proId"].ToString();
            productName = dr["productName"].ToString();
            unit = dr["unit"].ToString();
            price = dr["price"].ToString();
            orderQty = dr["orderQty"].ToString();
            totalQty = dr["totalQty"].ToString();
            //proImg = dr["proImg"].ToString();
        }
    }
}