using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace APIKwickWash.Models
{
    public class Profile
    {
        public int profileId { get; set; }
        public string dtmAdd { get; set; }
        public string dtmUpdate { get; set; }
        public string isValid { get; set; }
        public string isStatus { get; set; }
        public string userId { get; set; }
        public string name { get; set; }
        public string emailId { get; set; }
        public string mobile { get; set; }
        public string dob { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
        public string aadhaarNo { get; set; }
        public string bankName { get; set; }
        public string bankAccount { get; set; }
        public string bankIfsc { get; set; }
        public string bankBranch { get; set; }
        public string profileType { get; set; }
        public string gstNo { get; set; }
        public string companyLogo { get; set; }
        public string status { get; set; }
        public string vendorName { get; set; }
        public string password { get; set; }
        public string shopUserId { get; set; }
        public string Location { get; set; }
        public string areaId { get; set; }
        public decimal? balance { get; set; }
        public string flgIsSMS { get; set; }
    }
    public class CreateProfile : Profile
    {

    }

    public class ReadProfile : Profile
    {
        public ReadProfile(DataRow dr)
        {
            profileId = Convert.ToInt32(dr["profileId"]);
            dtmAdd = dr["dtmAdd"].ToString();
            dtmUpdate = dr["dtmUpdate"].ToString();
            isValid = dr["isValid"].ToString();
            isStatus = dr["isStatus"].ToString();
            userId = dr["userId"].ToString();
            name = dr["name"].ToString();
            emailId = dr["emailId"].ToString();
            mobile = dr["mobile"].ToString();
            dob = dr["dob"].ToString();
            address = dr["address"].ToString();
            state = dr["state"].ToString();
            city = dr["city"].ToString();
            pincode = dr["pincode"].ToString();
            aadhaarNo = dr["aadhaarNo"].ToString();
            bankName = dr["bankName"].ToString();
            bankAccount = dr["bankAccount"].ToString();
            bankIfsc = dr["bankIfsc"].ToString();
            bankBranch = dr["bankBranch"].ToString();
            profileType = dr["profileType"].ToString();
            gstNo = dr["gstNo"].ToString();
            companyLogo = dr["companyLogo"].ToString();
            status = dr["status"].ToString();
            Location = dr["Location"].ToString();
            shopUserId = dr["upLineId"].ToString();
            balance = Convert.ToDecimal(dr["balance"]);
            flgIsSMS = dr["flgIsSMS"].ToString();
        }
    }

    public class ReadProfile1 : Profile
    {
        public ReadProfile1(DataRow dr)
        {
            profileId = Convert.ToInt32(dr["profileId"]);
            dtmAdd = dr["dtmAdd"].ToString();
            dtmUpdate = dr["dtmUpdate"].ToString();
            isValid = dr["isValid"].ToString();
            isStatus = dr["isStatus"].ToString();
            userId = dr["userId"].ToString();
            name = dr["name"].ToString();
            emailId = dr["emailId"].ToString();
            mobile = dr["mobile"].ToString();
            dob = dr["dob"].ToString();
            address = dr["address"].ToString();
            state = dr["state"].ToString();
            city = dr["city"].ToString();
            pincode = dr["pincode"].ToString();
            aadhaarNo = dr["aadhaarNo"].ToString();
            bankName = dr["bankName"].ToString();
            bankAccount = dr["bankAccount"].ToString();
            bankIfsc = dr["bankIfsc"].ToString();
            bankBranch = dr["bankBranch"].ToString();
            profileType = dr["profileType"].ToString();
            gstNo = dr["gstNo"].ToString();
            companyLogo = dr["companyLogo"].ToString();
            status = dr["status"].ToString();
            vendorName = dr["VendorName"].ToString();
            balance = Convert.ToDecimal(dr["balance"]);
            flgIsSMS = dr["flgIsSMS"].ToString();
        }
    }
}