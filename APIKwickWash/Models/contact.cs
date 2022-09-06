using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace APIKwickWash.Models
{
    public class contact
    {
        public string contactId { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string mess { get; set; }
        public string status { get; set; }
        public string date { get; set; }
    }
    
    public class CreateContact:contact
    {

    }

    public class ReadContact:contact
    {
        public ReadContact(DataRow dr)
        {
            contactId = dr["contactId"].ToString();
            name = dr["name"].ToString();
            mobile = dr["mobile"].ToString();
            email = dr["email"].ToString();
            address = dr["address"].ToString();
            mess = dr["mess"].ToString();
            status = dr["status"].ToString();
            if (dr["dtCreatedDate"] != DBNull.Value)
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