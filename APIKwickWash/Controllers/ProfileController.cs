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
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace APIKwickWash.Controllers
{
    public class ProfileController : ApiController
    {
        /* GET api/<controller> */
        [System.Web.Mvc.HttpPost]
        public IEnumerable<Profile> Get()
        {
            string query = "select * from tbl.Profile";

            DataTable dt = Database.get_DataTable(query);
            List<Profile> profile = new List<Models.Profile>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    profile.Add(new ReadProfile(dr));
                }
            }
            return profile;
        }

        /* GET api/<controller>/id */
        public IEnumerable<Profile> Get(int id)
        {
            string query = "select * from tbl.Profile where userid='" + id.ToString() + "'";
            DataTable dt = Database.get_DataTable(query);
            List<Profile> profile = new List<Models.Profile>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    profile.Add(new ReadProfile(dr));
                }
            }
            return profile;
        }

        public List<Profile> GetCustomer(string mobile, string shopUserid)
        {
            string query;
            if (mobile.ToString() == "0")
            {
                query = "select * from tbl.profile where uplineid='" + shopUserid + "'";
            }
            else
            {
                query = "select * from tbl.profile where mobile='" + mobile + "'";
            }
            DataTable dt = Database.get_DataTable(query);
            List<Profile> profile = new List<Models.Profile>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    profile.Add(new ReadProfile(dr));
                }
            }
            return profile;
        }

        public class Response
        {
            public string message_id { get; set; }
            public int message_count { get; set; }
            public double price { get; set; }
        }
        public class RootObject
        {
            public Response Response { get; set; }
            public string ErrorMessage { get; set; }
            public int Status { get; set; }
        }

        public string Post([FromBody] CreateProfile values)
        {
            try
            {
                string query_profile = "", query_login = "", password = "", roles = "", upLineId = "", sqlQuery = "", res = "";
                int res1 = 0;
                if (values.profileId == 0)
                {
                    string shopId = "0";
                    if (values.shopUserId == "0")
                    {
                        string queryShop = "select shopId from tbl.area where areaId='" + values.areaId + "'";
                        DataSet dsShopId = Database.get_DataSet(queryShop);
                        if (dsShopId.Tables[0].Rows.Count > 0)
                        {
                            shopId = dsShopId.Tables[0].Rows[0]["shopId"].ToString();
                        }
                        password = values.password;
                    }
                    else
                    {
                        shopId = values.shopUserId;
                        password = "pass@123";
                    }


                    roles = "2";
                    upLineId = "0";
                    string cname = values.name;
                    cname = cname.Replace("`", "");
                    query_login = "insert into tbl.login (username,password,name,email,role,status) values ('" + values.mobile + "','" + password.ToString()
                        + "','" + cname.ToString() + "','" + values.emailId + "','" + roles.ToString() + "','1')";

                    query_profile = "declare @Userid bigint select @Userid=IDENT_CURRENT('tbl.login')";

                    query_profile += "insert into tbl.Profile(userId,name,emailId,mobile,address,state,city,pincode,companyLogo,upLineId,Location) values " +
                        "(@Userid,'" + cname.ToString() + "','" + values.emailId + "','" + values.mobile + "','" + values.address
                        + "','" + values.state + "','" + values.city + "','" + values.pincode + "','" + values.companyLogo + "','" + shopId
                        + "','" + values.Location + "')";
                    res = Database.Execute_Transaction(query_login, query_profile);
                }
                else
                {
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

                    query_profile = "update tbl.Profile set " + sqlQuery + " dtmUpdate='" + DateTime.Now.ToString() + "' where profileId='" + values.profileId + "'";
                    res1 = Database.Execute(query_profile);
                }

                if (res == "1" || res1 == 1)
                {
                    if (res == "1")
                    {


                        //Call Send SMS API  
                        string sendSMSUri = "http://m1.sarv.com/api/v2.0/sms_campaign.php?token=705915705611ff6c4c5f9d8.90022694&user_id=63515991&route=TR&template_id=5571&sender_id=JKWASH&language=EN&template=Thank+you+for+sign+in+with+KwickWash+Laundry.+We+welcome+you+for+amazing+hygienic+services.&contact_numbers=" + values.mobile.ToString();

                        //Create HTTPWebrequest  
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                       
                        //Specify post method  
                        httpWReq.Method = "POST";
                        httpWReq.ContentType = "application/x-www-form-urlencoded";
                        //Get the response  
                        httpWReq.Timeout = 10000;

                        HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string responseString = reader.ReadToEnd();

                        //Close the response  
                        reader.Close();

                        response.Close();
                    }
                    string userId = "";
                    if (values.shopUserId == "0")
                    {
                        userId = "121";
                    }
                    else
                    {
                        userId = values.shopUserId;
                    }
                    int rest = Database.Dashboard("ttlCustomer", "1", userId);
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "-1" + ex.ToString();
            }
        }
    }
}