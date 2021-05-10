using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace APIKwickWash.Models
{
    public class banner
    {
        public string bid { get; set; }
        public string bannerImg { get; set; }
        public string urlLink { get; set; }
        public string status { get; set; }
    }

    public class CreateBanner:banner
    {

    }

    public class ReadBanner:banner
    {
        public ReadBanner(DataRow dr)
        {
            bid = dr["bid"].ToString();
            bannerImg = dr["bannerImg"].ToString();
            urlLink = dr["urlLink"].ToString();
            status = dr["status"].ToString();
        }
    }
}