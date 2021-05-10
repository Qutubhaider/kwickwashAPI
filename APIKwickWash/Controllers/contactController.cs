using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIKwickWash.Models;

namespace APIKwickWash.Controllers
{
    public class contactController : ApiController
    {
        public IEnumerable<contact> Get()
        {
            string query = "select * from tbl.contact";
            DataTable dt = Database.get_DataTable(query);
            List<contact> contact = new List<Models.contact>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    contact.Add(new ReadContact(dr));
                }
            }
            return contact;
        }

        public string Post([FromBody] CreateContact values)
        {
            try
            {
                string query_contact = "";
                if (values.contactId == "0")
                {
                    query_contact = "insert into tbl.contact (name,mobile,email,address,mess) values ('" + values.name
                        + "','" + values.mobile + "','" + values.email + "','" + values.address + "','" + values.mess + "')";
                }
                else
                {
                    query_contact = "update tbl.contact set name='" + values.name + "', mobile='" + values.mobile
                        + "', email='" + values.email + "',address='" + values.address + "',mess='" + values.mess
                        + "' where contactId='" + values.contactId + "'";
                }
                int res = Database.Execute(query_contact);
                if (res == 1)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }

        }
    }
}
