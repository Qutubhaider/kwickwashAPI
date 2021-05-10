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
    public class CompanyProfileController : ApiController
    {
        // GET: CompanyProfile
        public IEnumerable<CompanyProfile> Get()
        {
            string query = "select * from tbl.CompanyProfile";

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
            string query = "select * from tbl.CompanyProfile where companyProfileId='" + id.ToString() + "'";
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


        public IEnumerable<CompanyProfile> GetCompanyData(int id, string status)
        {
            string query = "select * from tbl.CompanyProfile where userid='" + id + "'";
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

        public List<CompanyProfile> GetShopList(int pincode, string status,string shop)
        {
            string query = "";
            if (pincode==0)
            {
                query = "select * from tbl.CompanyProfile where VendorId='2'";
            }
            else
            {
                query = "select * from tbl.CompanyProfile where CommunicationPincode='" + pincode + "' and VendorId='2'";
            }
           
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


        public string Post([FromBody]CreateCompanyProfile values)
        {
            try
            {
                string query_CompanyProfile = "", query_login = "", password = "", roles = "", VendorId = "", res = "";
                int res1 = 0;
                if (values.companyProfileId == 0)
                {
                    if (values.vendorId == "0")
                    {
                        password = "pass@123";
                        roles = "5";
                        VendorId = "0";
                    }
                    else if (values.vendorId == "1")
                    {
                        password = "pass@123";
                        roles = "3";
                        VendorId = "1";
                    }
                    else if (values.vendorId == "2")
                    {
                        password = "pass@123";
                        roles = "3";
                        VendorId = "2";
                    }

                    query_login = "insert into tbl.login (username,password,name,email,role,status) values ('" + values.Mobile + "','" + password.ToString()
                        + "','" + values.AuthorizedPersonName + "','" + values.Email + "','" + roles.ToString() + "','1')";

                    query_CompanyProfile = "declare @Userid bigint select @Userid=IDENT_CURRENT('tbl.login')";

                    query_CompanyProfile += "insert into tbl.CompanyProfile (dtmAdd,dtmUpdate,isValid,isStatus,userId,vendorId,profileType,companyType," +
                        "companyName,companyMobile,companyEmail,companyRegistrationNo,companyRegistrationDocument,companyGSTno,companyGSTimg," +
                        "companyPAN,companyPANimg,AuthorizedPersonName,Email,Mobile,AadharNo,AadharImg,PANno,PANimg,CommunicationAddress," +
                        "CommunicationState,CommunicationCity,CommunicationPincode,RegAddess,RegState,RegCity,RegPincode,BankName,BankAccount," +
                        "BankIFSC,BankBranch,LogoPhoto,GSTno,GSTimg,Latitude,Longitude,Status,Location,Location1,Area) values ('" + DateTime.Now.ToString()
                        + "','" + DateTime.Now.ToString() + "','0','0',@Userid,'" + VendorId + "','" + values.profileType + "','" + values.companyType
                        + "','" + values.companyName + "','" + values.companyMobile + "','" + values.companyEmail + "','" + values.companyRegistrationNo + "','" + values.companyRegistrationDocument
                        + "','" + values.companyGSTno + "','" + values.companyGSTimg + "','" + values.companyPAN + "','" + values.companyPANimg + "','" + values.AuthorizedPersonName
                        + "','" + values.Email + "','" + values.Mobile + "','" + values.AadharNo + "','" + values.AadharImg + "','" + values.PANno + "','" + values.PANimg
                        + "','" + values.CommunicationAddress + "','" + values.CommunicationState + "','" + values.CommunicationCity + "','" + values.CommunicationPincode
                        + "','" + values.RegAddess + "','" + values.RegState + "','" + values.RegCity + "','" + values.RegPincode + "','" + values.BankName + "','" + values.BankAccount
                        + "','" + values.BankIFSC + "','" + values.BankBranch + "','" + values.LogoPhoto + "','" + values.GSTno + "','" + values.GSTimg + "','" + values.Latitude
                        + "','" + values.Longitude + "','" + values.Status + "','" + values.Location + "','" + values.Location1 + "','" + values.Area + "')";
                    res = Database.Execute_Transaction(query_login, query_CompanyProfile);
                }
                else
                {
                    query_CompanyProfile = "update tbl.CompanyProfile set dtmUpdate='" + DateTime.Now.ToString() + "',vendorId='" + values.vendorId
                        + "',profileType='" + values.profileType + "',companyName='" + values.companyName + "',companyEmail='" + values.companyEmail
                        + "',companyMobile='" + values.companyMobile + "',companyRegistrationNo='" + values.companyRegistrationNo
                        + "',companyRegistrationDocument='" + values.companyRegistrationDocument + "',companyGSTno='" + values.companyGSTno + "',companyGSTimg='" + values.companyGSTimg
                        + "',companyPAN='" + values.companyPAN + "',companyPANimg='" + values.companyPANimg + "',AuthorizedPersonName='" + values.AuthorizedPersonName
                        + "',Email='" + values.Email + "',Mobile='" + values.Mobile + "',AadharNo='" + values.AadharNo + "',PANno='" + values.PANno
                        + "',PANimg='" + values.PANimg + "',CommunicationAddress='" + values.CommunicationAddress + "',CommunicationState='" + values.CommunicationState
                        + "',CommunicationCity='" + values.CommunicationCity + "',CommunicationPincode='" + values.CommunicationPincode + "',RegAddess='" + values.RegAddess
                        + "',RegState='" + values.RegState + "',RegCity='" + values.RegCity + "',RegPincode='" + values.RegPincode + "',BankName='" + values.BankName
                        + "',BankAccount='" + values.BankAccount + "',BankIFSC='" + values.BankIFSC + "',BankBranch='" + values.BankBranch + "',LogoPhoto='" + values.LogoPhoto
                        + "',GSTno='" + values.GSTno + "',GSTimg='" + values.GSTimg + "',Latitude='" + values.Latitude + "',Longitude='" + values.Longitude
                        + "',Location='" + values.Location + "',Location1='" + values.Location1 + "',Area='" + values.Area
                        + "' where companyProfileId='" + values.companyProfileId + "'";

                    res1 = Database.Execute(query_CompanyProfile);
                }

                if (res == "1" || res1 == 1)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "-1";
            }

        }
    }
}