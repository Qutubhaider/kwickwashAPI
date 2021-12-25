using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIKwickWash.Models;
using System.Web.Cors;
using System.Web.Http.Cors;
using System.IO;

namespace APIKwickWash.Controllers
{
    public class SignupController : ApiController
    {
        // GET: Signup
        [HttpPost]
        public string Post([FromBody]CreateLogin values)
        {
            try
            {
                string query_login = "insert into tbl.login (username,password,name,email,role,status,phone) values ('" + values.mobile + "','" + values.password.ToString()
                           + "','" + values.name + "','" + values.email + "','5','1','" + values.mobile + "')";
                int res = Database.Execute(query_login);
                if (res == 1)
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