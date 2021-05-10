using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace APIKwickWash.Models
{
    public class area
    {
        public string areaId { get; set; }
        public string areaName { get; set; }
        public string cityId { get; set; }
        public string cityName { get; set; }
        public string status { get; set; }
        public string shopId { get; set; }
        public string status1 { get; set; }
    }

    public class CreateArea:area
    {

    }

    public class ReadArea:area
    {
        public ReadArea(DataRow dr)
        {
            areaId = dr["areaId"].ToString();
            areaName = dr["areaName"].ToString();
            cityId = dr["cityId"].ToString();
            cityName = dr["cityName"].ToString();
            status = dr["status"].ToString();
            shopId = dr["shopId"].ToString();
            status1 = dr["status1"].ToString();
        }
    }
}