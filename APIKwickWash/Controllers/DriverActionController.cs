using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using APIKwickWash.Models;
namespace APIKwickWash.Controllers
{
    
    public class DriverActionController : Controller
    {
        public string Post([FromBody] CreateAccept Data)
        {
            string res = string.Empty;
            try
            {
                string query = "INSERT INTO TBL.KWICKORDER isAccept='" + Data.isAccept + "' where KOID='" + Data.orderId + "'";
                int result = Database.Execute(query);
                if (result == 1)
                {
                    res = "1";
                }
                else
                {
                    res = "0";
                }
            }
            catch(Exception ex)
            {
                res = "00";
            }
            return res;
        }
    }
}
