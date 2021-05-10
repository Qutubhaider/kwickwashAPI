using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace APIKwickWash.Models
{
    public class CompanyProfile
    {
        public int companyProfileId { get; set; }
        public string dtmAdd { get; set; }
        public string dtmUpdate { get; set; }
        public string isValid { get; set; }
        public string isStatus { get; set; }
        public string userId { get; set; }       
        public string vendorId { get; set; }
        public string profileType { get; set; }
        public string companyType { get; set; }
        public string companyName { get; set; }
        public string companyMobile { get; set; }
        public string companyEmail { get; set; }
        public string companyRegistrationNo { get; set; }
        public string companyRegistrationDocument { get; set; }
        public string companyGSTno { get; set; }
        public string companyGSTimg { get; set; }
        public string companyPAN { get; set; }
        public string companyPANimg { get; set; }
        public string AuthorizedPersonName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AadharNo { get; set; }
        public string AadharImg { get; set; }
        public string PANno { get; set; }
        public string PANimg { get; set; }
        public string CommunicationAddress { get; set; }
        public string CommunicationState { get; set; }
        public string CommunicationCity { get; set; }
        public string CommunicationPincode { get; set; }
        public string RegAddess { get; set; }
        public string RegState { get; set; }
        public string RegCity { get; set; }
        public string RegPincode { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string BankIFSC { get; set; }
        public string BankBranch { get; set; }
        public string LogoPhoto { get; set; }
        public string GSTno { get; set; }
        public string GSTimg { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string Location1 { get; set; }
        public string Area { get; set; }

    }
    
    public class CreateCompanyProfile : CompanyProfile
    {

    }

    public class ReadCompanyProfile: CompanyProfile
    {
        public ReadCompanyProfile(DataRow dr)
        {
            companyProfileId = Convert.ToInt32(dr["companyProfileId"]);
            dtmAdd = dr["dtmAdd"].ToString();
            dtmUpdate = dr["dtmUpdate"].ToString();
            isValid = dr["isValid"].ToString();
            isStatus = dr["isStatus"].ToString();
            userId = dr["userId"].ToString();            
            vendorId = dr["vendorId"].ToString();
            profileType = dr["profileType"].ToString();
            companyType = dr["companyType"].ToString();
            companyName = dr["companyName"].ToString();
            companyMobile = dr["companyMobile"].ToString();
            companyEmail = dr["companyEmail"].ToString();
            companyRegistrationNo = dr["companyRegistrationNo"].ToString();
            companyRegistrationDocument = dr["companyRegistrationDocument"].ToString();
            companyGSTno = dr["companyGSTno"].ToString();
            companyGSTimg = dr["companyGSTimg"].ToString();
            companyPAN = dr["companyPAN"].ToString();
            companyPANimg = dr["companyPANimg"].ToString();
            AuthorizedPersonName = dr["AuthorizedPersonName"].ToString();
            Email = dr["Email"].ToString();
            Mobile = dr["Mobile"].ToString();
            AadharNo = dr["AadharNo"].ToString();
            AadharImg = dr["AadharImg"].ToString();
            PANno = dr["PANno"].ToString();
            PANimg = dr["PANimg"].ToString();
            CommunicationAddress = dr["CommunicationAddress"].ToString();
            CommunicationState = dr["CommunicationState"].ToString();
            CommunicationCity = dr["CommunicationCity"].ToString();
            CommunicationPincode = dr["CommunicationPincode"].ToString();
            RegAddess = dr["RegAddess"].ToString();
            RegState = dr["RegState"].ToString();
            RegCity = dr["RegCity"].ToString();
            RegPincode = dr["RegPincode"].ToString();
            BankName = dr["BankName"].ToString();
            BankAccount = dr["BankAccount"].ToString();
            BankIFSC = dr["BankIFSC"].ToString();
            BankBranch = dr["LogoPhoto"].ToString();
            LogoPhoto = dr["LogoPhoto"].ToString();
            GSTno = dr["GSTno"].ToString();
            GSTimg = dr["GSTimg"].ToString();
            Latitude = dr["Latitude"].ToString();
            Longitude = dr["Longitude"].ToString();
            Status = dr["Status"].ToString();
            Location = dr["Location"].ToString();
            Location1 = dr["Location1"].ToString();
            Area = dr["Area"].ToString();
        }
    }
}