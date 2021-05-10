using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace APIKwickWash.Models
{
    public class Vers
    {
        public int versionId { get; set; }
        public string dtmAdd { get; set; }
        public string dtmUpdate { get; set; }
        public string isValid { get; set; }
        public string lastVersion { get; set; }
        public string currentVersion { get; set; }
    }
    public class Createvers : Vers
    {
    }

    public class Readvers : Vers
    {
        public Readvers(DataRow dr)
        {
            versionId = Convert.ToInt32(dr["versionId"]);
            dtmAdd = dr["dtmAdd"].ToString();
            dtmUpdate = dr["dtmUpdate"].ToString();
            isValid = dr["isValid"].ToString();
            lastVersion = dr["lastVersion"].ToString();
            currentVersion = dr["currentVersion"].ToString();
        }
    }
}