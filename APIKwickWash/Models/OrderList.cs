using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace APIKwickWash.Models
{
    public class OrderList
    {
        public string orderId { get; set; }
        public string invoiceNo { get; set; }
        public string ttlQty { get; set; }
        public string ttlAmount { get; set; }
        public string ttlDiscount { get; set; }
        public string ttlPayableAmount { get; set; }
        public string Status { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string orderDate { get; set; }
        public string deliveryDate { get; set; }
        public string OrderType { get; set; }
        public string pickupRequest { get; set; }
        public string dropRequest { get; set; }
        public string pickupSlip { get; set; }
        public string paymentMode { get; set; }
        public string deliveryStatus { get; set; }
        public string companyName { get; set; }
        public string addonName { get; set; }
        public string addonQty { get; set; }
        public string addonPrice { get; set; }
        public string addonTotalPrice { get; set; }

    }

    public class ReadOrderList: OrderList
    {
        public ReadOrderList(DataRow dr)
        {
            orderId = dr["orderId"].ToString();
            invoiceNo = dr["invoiceNo"].ToString();
            ttlQty = dr["ttlQty"].ToString();
            ttlAmount = dr["ttlAmount"].ToString();
            Status = dr["Status"].ToString();
            name = dr["name"].ToString();
            mobile = dr["mobile"].ToString();
            DateTime oDate = Convert.ToDateTime(dr["orderDate"]);
            orderDate = oDate.ToString("dd-MMM-yyyy hh:mm").ToString();
            oDate = Convert.ToDateTime(dr["deliveryDate"]);
            deliveryDate = oDate.ToString("dd-MMM-yyyy hh:mm").ToString();
            OrderType = dr["OrderType"].ToString();
            pickupRequest = dr["pickupRequest"].ToString();
            dropRequest = dr["dropRequest"].ToString();
            pickupSlip = dr["pickupSlip"].ToString();
            paymentMode = dr["paymentMode"].ToString();
            deliveryStatus = dr["deliveryStatus"].ToString();
            ttlDiscount = dr["ttlDiscount"].ToString();
            ttlPayableAmount = dr["ttlPayableAmount"].ToString();
            companyName = dr["companyName"].ToString();
            addonName = dr["ProdctName"].ToString();
            addonQty = dr["Qty"].ToString();
            addonPrice = dr["Price"].ToString();
            addonTotalPrice = dr["TotalPrice"].ToString();
        }
    }
}