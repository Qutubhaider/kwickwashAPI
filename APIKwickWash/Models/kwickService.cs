using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace APIKwickWash.Models
{
    public class kwickService
    {
        public int sid { get; set; }
        public string serviceName { get; set; }
        public string serviceDescription { get; set; }
        public string serviceImg { get; set; }
        public string serviceStatus { get; set; }
        public string startAt { get; set; }
        public string duration { get; set; }
        public string Unit { get; set; }
    }

    public class CreateService : kwickService
    {

    }

    public class ReadService : kwickService
    {
        public ReadService(DataRow dr)
        {
            sid = Convert.ToInt32(dr["srid"]);
            serviceName = dr["serviceName"].ToString();
            serviceDescription = dr["serviceDescription"].ToString();
            serviceImg = dr["serviceImg"].ToString();
            serviceStatus = dr["status"].ToString();
            startAt = dr["startAt"].ToString();
            duration = dr["duration"].ToString();
            Unit = dr["Unit"].ToString();
        }
    }
}