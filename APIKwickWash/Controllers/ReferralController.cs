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
    public class ReferralController : ApiController
    {
        // GET: Referral
        public IEnumerable<CompanyProfile> Get()
        {
            string query = "select * from tbl.CompanyProfile where vendorId='1'";

            DataTable dt = Database.get_DataTable(query);
            List<CompanyProfile> companyProfiles = new List<Models.CompanyProfile>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    companyProfiles.Add(new ReadCompanyProfile(dr));
                }
            }
            return companyProfiles;
        }

        public IEnumerable<CompanyProfile> Get(int id)
        {
            string query = "select * from tbl.CompanyProfile where companyProfileId='" + id.ToString() + "' and vendorId='1'";
            DataTable dt = Database.get_DataTable(query);
            List<CompanyProfile> profile = new List<Models.CompanyProfile>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    profile.Add(new ReadCompanyProfile(dr));
                }
            }
            return profile;
        }
    }
}