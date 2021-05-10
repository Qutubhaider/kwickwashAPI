using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace APIKwickWash.Models
{
    public class city
    {
        public string cityId { get; set; }
        public string cityName { get; set; }
        public string status { get; set; }
    }

    public class CreateCity:city
    {

    }

    public class ReadCity:city
    {
        public ReadCity(DataRow dr)
        {
            cityId = dr["cityId"].ToString();
            cityName = dr["cityName"].ToString();
            status = dr["status"].ToString();
        }
    }
}