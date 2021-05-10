using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIKwickWash.Models
{
    public class kwickorderapp
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
        public string userId { get; set; }
    }
    public class Createkwickorderapp : kwickorderapp
    {

    }
}