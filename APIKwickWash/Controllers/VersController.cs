using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;

using APIKwickWash.Models;

namespace APIKwickWash.Controllers
{
    public class VersController : ApiController
    {
        // GET: Vers
        /* GET api/<controller> */
        public IEnumerable<Vers> Get()
        {
            string query = "select * from tbl.Versions";
            DataTable dt = Database.get_DataTable(query);
            List<Vers> vers = new List<Vers>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    vers.Add(new Readvers(dr));
                }
            }
            return vers;
        }


        public string Post([FromBody]Createvers value)
        {
            //TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            //DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            string query = "insert into tbl.Versions(dtmAdd,dtmUpdate,isValid,lastVersion,currentVersion) values";
            query += " ('" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + value.isValid + "','" + value.lastVersion + "','" + value.currentVersion + "')";
            int res = Database.Execute(query);
            if (res == 1)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        public string Put(int id, [FromBody]Createvers value)
        {
            string query = "update tbl.Versions set dtmUpdate='" + value.dtmUpdate + "', lastVersion='" + value.lastVersion + "',currentVersion='" + value.currentVersion
                + "',isValid='" + value.isValid + "' where versionId='" + id + "'";
            int res = Database.Execute(query);
            if (res == 1)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        /* DELETE api/<controller>/1 */
        public string Delete(int id)
        {
            string query = "delete from tbl.Versions where versionId='" + id + "'";
            int res = Database.Execute(query);
            if (res == 1)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
    }
}