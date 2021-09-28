using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace APIKwickWash.Models
{
    public class kwickOrder
    {
        public string koId { get; set; }
        public string cityId { get; set; }
        public string cityName { get; set; }
        public string areaId { get; set; }
        public string areaName { get; set; }
        public string Location { get; set; }
        public string srId { get; set; }
        public string serviceName { get; set; }
        public string customerName { get; set; }
        public string mobile { get; set; }
        public string orderDate { get; set; }
        public string shopId { get; set; }
        public string lat { get; set; }
        public string longs { get; set; }
        public string companyName { get; set; }
        public string did { get; set; }
        public string dName { get; set; }
        public string isAccept { get; set; }
    }

    public class CreatekwickOrder:kwickOrder
    {

    }

    public class ReadKwickOrder : kwickOrder
    {
        public ReadKwickOrder(DataRow dr)
        {
            koId = dr["koId"].ToString();
            cityId = dr["cityId"].ToString();
            cityName = dr["cityName"].ToString();
            areaId = dr["areaId"].ToString();
            areaName = dr["areaName"].ToString();
            Location = dr["Location"].ToString();
            srId = dr["srId"].ToString();
            serviceName = dr["serviceName"].ToString();
            customerName = dr["customerName"].ToString();
            mobile = dr["mobile"].ToString();
            orderDate = dr["orderDate"].ToString();
            shopId = dr["shopId"].ToString();
            lat = dr["lat"].ToString();
            longs = dr["longs"].ToString();
            dName = dr["dName"].ToString();
            isAccept = dr["isAccept"].ToString();
        }
    }

    public class ReadKwickOrderDt : kwickOrder
    {
        public ReadKwickOrderDt(DataRow dr)
        {
            koId = dr["koId"].ToString();
            cityId = dr["cityId"].ToString();
            cityName = dr["cityName"].ToString();
            areaId = dr["areaId"].ToString();
            areaName = dr["areaName"].ToString();
            Location = dr["Location"].ToString();
            srId = dr["srId"].ToString();
            serviceName = dr["serviceName"].ToString();
            customerName = dr["customerName"].ToString();
            mobile = dr["mobile"].ToString();
            DateTime odt = Convert.ToDateTime(dr["orderDate"]);
            orderDate = odt.ToString("dd-MMM-yyyy hh:mm").ToString();
            shopId = dr["shopId"].ToString();
            lat = dr["lat"].ToString();
            longs = dr["longs"].ToString();
            companyName = dr["companyName"].ToString();
            dName = dr["dName"].ToString();
            isAccept = dr["isAccept"].ToString();
        }
    }
}