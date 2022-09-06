using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace APIKwickWash.Models
{
    public class Login
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string role { get; set; }
    }

    public class OTPLogin
    {
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }

    public class LoginWithOTP
    {
        public string userid { get; set; }
        public string otp { get; set; }
    }

    public class CreateLoginWithOTP : LoginWithOTP
    {
    }


    public class CreateOTPLogin : OTPLogin
    {

    }
    public class CreateLogin : Login
    {
    }

    public class ReadLogin : Login
    {
        public ReadLogin(DataRow dr)
        {
            userId = Convert.ToInt32(dr["userId"]);
            username = dr["username"].ToString();
            password = dr["password"].ToString();
            name = dr["name"].ToString();
            email = dr["email"].ToString();
            mobile = dr["phone"].ToString();
            role = dr["role"].ToString();
        }
    }
}