using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIKwickWash.Models;

namespace APIKwickWash.Controllers
{
    public class AppProfileController : ApiController
    {
        public string Post([FromBody] CreateProfile values)
        {
            try
            {
                string query_profile = "", sqlQuery = "", res = "";
                int res1 = 0;

                if (values.name != null)
                {
                    sqlQuery += "name='" + values.name + "',";
                }

                if (values.emailId != null)
                {
                    sqlQuery += "emailId='" + values.emailId + "',";
                }

                if (values.mobile != null)
                {
                    sqlQuery += "mobile='" + values.mobile + "',";
                }

                if (values.dob != null)
                {
                    sqlQuery += "dob='" + values.dob + "',";
                }

                if (values.address != null)
                {
                    sqlQuery += "address='" + values.address + "',";
                }

                if (values.state != null)
                {
                    sqlQuery += "state='" + values.state + "',";
                }

                if (values.city != null)
                {
                    sqlQuery += "city='" + values.city + "',";
                }

                if (values.pincode != null)
                {
                    sqlQuery += "pincode='" + values.pincode + "',";
                }

                if (values.aadhaarNo != null)
                {
                    sqlQuery += "aadhaarNo='" + values.aadhaarNo + "',";
                }

                if (values.aadhaarNo != null)
                {
                    sqlQuery += "bankName='" + values.bankName + "',";
                }

                if (values.bankAccount != null)
                {
                    sqlQuery += "bankAccount='" + values.bankAccount + "',";
                }

                if (values.bankIfsc != null)
                {
                    sqlQuery += "bankIfsc='" + values.bankIfsc + "',";
                }

                if (values.bankBranch != null)
                {
                    sqlQuery += "bankBranch='" + values.bankBranch + "',";
                }

                if (values.gstNo != null)
                {
                    sqlQuery += "gstNo='" + values.gstNo + "',";
                }

                if (values.companyLogo != null)
                {
                    sqlQuery += "companyLogo='" + values.companyLogo + "',";
                }

                if (values.Location != null)
                {
                    sqlQuery += "Location='" + values.Location + "',";
                }

                query_profile = "update tbl.Profile set " + sqlQuery + " dtmUpdate='" + DateTime.Now.ToString() + "' " +
                    "where profileId in " +
                    "(select profileId from tbl.Profile where userid='" + values.profileId + "')";
                res1 = Database.Execute(query_profile);


                if (res == "1" || res1 == 1)
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
                return "-1";
            }
        }
    }
}
