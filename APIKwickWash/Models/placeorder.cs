using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Drawing.Printing;

namespace APIKwickWash.Models
{
    public class placeorder
    {
        public string orderItemId { get; set; }
        public string orderId { get; set; }
        public string CUserid { get; set; }
        public string SUserid { get; set; }
        public string invoiceNo { get; set; }
        public string srId { get; set; }
        public string serviceName { get; set; }
        public string proId { get; set; }
        public string productName { get; set; }
        public string unit { get; set; }
        public string price { get; set; }
        public string orderQty { get; set; }
        public string status { get; set; }
        public string orderDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public string OrderType { get; set; }
        public string pickupRequest { get; set; }
        public string dropRequest { get; set; }
        public string pickupSlip { get; set; }
        public string totalQty { get; set; }
        public string paymentMode { get; set; }
        public string deliveryStatus { get; set; }
        public string useCoupon { get; set; }
        public string productCode { get; set; }
        public string serviceCode { get; set; }
        public string did { get; set; }
        public string dname { get; set; }
        public string AddonProduct { get; set; }
        public string AddonQty { get; set; }
        public string AddonPrice { get; set; }
        public string AddonTotalPrice { get; set; }
        public string AdvanceAmount { get; set; }
    }

    public class Createplaceorder: placeorder
    {

    }


    public class Readplaceorder1 : placeorder
    {
        public Readplaceorder1(DataRow dr)
        {
            orderItemId = dr["orderItemId"].ToString();
            orderId = dr["orderId"].ToString();
            CUserid = dr["CUserid"].ToString();
            SUserid = dr["SUserid"].ToString();
            invoiceNo = dr["invoiceNo"].ToString();
            srId = dr["srId"].ToString();
            serviceName = dr["serviceName"].ToString();
            proId = dr["proId"].ToString();
            productName = dr["productName"].ToString();
            unit = dr["unit"].ToString();
            price = dr["price"].ToString();
            orderQty = dr["orderQty"].ToString();
            totalQty = dr["totalQty"].ToString();
            productCode = dr["productCode"].ToString();
            serviceCode = dr["serviceDescription"].ToString();
        }
    }

    public class Readplaceorder: placeorder
    {
        public Readplaceorder(DataRow dr)
        {
            orderItemId = dr["orderItemId"].ToString();
            orderId = dr["orderId"].ToString();
            CUserid = dr["CUserid"].ToString();
            SUserid = dr["SUserid"].ToString();
            invoiceNo = dr["invoiceNo"].ToString();
            srId = dr["srId"].ToString();
            serviceName = dr["serviceName"].ToString();
            proId = dr["proId"].ToString();
            productName = dr["productName"].ToString();
            unit = dr["unit"].ToString();
            price = dr["price"].ToString();
            orderQty = dr["orderQty"].ToString();
            status = dr["status"].ToString();
            totalQty = dr["totalQty"].ToString();
        }
    }
}