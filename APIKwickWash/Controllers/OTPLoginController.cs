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
    public class OTPLoginController : ApiController
    {

        private static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }
        public string Post([FromBody] CreateOTPLogin val)
        {
            string query = "select * from tbl.login where username='" + val.username + "' and role='2'";
            DataSet ds = Database.get_DataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string otp = GenerateNewRandom();
                string queryInsertOTP = "INSERT INTO tblOTPByUser(OTP,USERID,MOBILE) VALUES('" + otp + "','" + ds.Tables[0].Rows[0]["UserId"].ToString() + "','" + val.username + "')";
                int resOTP = Database.Execute(queryInsertOTP);
                if (resOTP == 1)
                {
                    string sendSMSAPI = "https://m1.sarv.com/api/v2.0/sms_campaign.php?token=705915705611ff6c4c5f9d8.90022694&user_id=63515991&route=OT&template_id=8980&sender_id=KWwash&language=EN&template=Dear+Qutub+OTP+to+login+your+account+is+223344+and+is+valid+for+10+minutes.+Thank+you+for+choosing+Kwickwash.+Visit+Again+www.kwickwash.in&contact_numbers=7004282924";
                    sendSMSAPI = sendSMSAPI.Replace("Qutub", ds.Tables[0].Rows[0]["name"].ToString().Trim());
                    sendSMSAPI = sendSMSAPI.Replace("7004282924", val.username);
                    sendSMSAPI = sendSMSAPI.Replace("223344", otp);
                    HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSAPI);
                    httpWReq.Method = "POST";
                    httpWReq.ContentType = "application/x-www-form-urlencoded";
                    httpWReq.Timeout = 10000;
                    HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseString = reader.ReadToEnd();
                    reader.Close();
                    response.Close();

                    return ds.Tables[0].Rows[0]["UserId"].ToString();
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
    }
}