using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace APIKwickWash.Models
{
    public class franchise
    {
        public string franchiseId { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string city { get; set; }
        public string status { get; set; }
        public string date { get; set; }
    }

    public class CreateFranchise: franchise
    {

    }

    public class ReadFranchise: franchise
    {
        public ReadFranchise(DataRow dr)
        {
            franchiseId = dr["franchiseId"].ToString();
            name = dr["name"].ToString();
            mobile = dr["mobile"].ToString();
            email = dr["email"].ToString();
            city = dr["city"].ToString();
            status = dr["status"].ToString();
            if (dr["dtCreatedDate"]!= DBNull.Value)
            {
                DateTime dtCreateDate = Convert.ToDateTime(dr["dtCreatedDate"]);
                date = dtCreateDate.ToString("dd-MMM-yyyy").ToString();
            }
            else
            {
                date = "NULL";
            }
        }
    }
}