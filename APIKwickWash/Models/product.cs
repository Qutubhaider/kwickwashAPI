using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace APIKwickWash.Models
{
    public class product
    {
        public string proId { get; set; }
        public string srId { get; set; }
        public string serviceName { get; set; }
        public string productName { get; set; }
        public string unit { get; set; }
        public string status { get; set; }
        public string price { get; set; }
        public string proImg { get; set; }
        public string productCode { get; set; }
        public string dropOffPrice { get; set; }
        public string pickupDropPrice { get; set; }
        public string productQty { get; set; }
        public string minOrder { get; set; }
    }

    public class CreateProduct:product
    {

    }

    public class ReadProduct:product
    {
        public ReadProduct(DataRow dr)
        {
            proId = dr["proId"].ToString();
            srId = dr["srId"].ToString();
            serviceName = dr["serviceName"].ToString();
            productName = dr["productName"].ToString();
            unit = dr["unit"].ToString();
            status = dr["status"].ToString();
            price = dr["price"].ToString();
            proImg = dr["proImg"].ToString();
            productCode = dr["productCode"].ToString();
            dropOffPrice = dr["dropOffPrice"].ToString();
            pickupDropPrice = dr["pickupDropPrice"].ToString();
            productQty = dr["productQty"].ToString();
            minOrder = dr["minOrder"].ToString();
        }
    }
}