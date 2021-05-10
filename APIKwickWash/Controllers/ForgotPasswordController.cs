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
using System.Configuration;
using System.Net.Mail;

namespace APIKwickWash.Controllers
{
    public class ForgotPasswordController : ApiController
    {
       
        public string Post([FromBody] CreateForgotPassword data)
        {
            string result = "0";
            try
            {
                string query = "select * from tbl.login where email='" + data.email + "' and role='" + data.role + "'";
                DataSet ds = Database.get_DataSet(query);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string Email_Body = "";
                    string path = "~/EmailTemplate/contact.html";
                    Email_Body = Function.ReadMailBody(path);
                    Email_Body = Email_Body.Replace("##Name##", ds.Tables[0].Rows[0]["NAME"].ToString());
                    Email_Body = Email_Body.Replace("##Username##", ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    Email_Body = Email_Body.Replace("##Password##", ds.Tables[0].Rows[0]["PASSWORD"].ToString());
                    result = Function.SendEmail(Email_Body, "", data.email);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result = "0";
                return result;
            }
            return result;
        }

      
    }
}
