using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIKwickWash.Models
{
    public class ForgotPassword
    {
        public string email { get; set; }
        public string role { get; set; }
    }

    public class CreateForgotPassword : ForgotPassword
    {

    }
}