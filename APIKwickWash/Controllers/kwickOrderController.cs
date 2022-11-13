using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIKwickWash.Models;

namespace APIKwickWash.Controllers
{
    public class kwickOrderController : ApiController
    {
        public IEnumerable<kwickOrder> Get()
        {
            string query = "select * from tbl.kwickOrder order by koId desc";
            DataTable dt = Database.get_DataTable(query);
            List<kwickOrder> kOrder = new List<Models.kwickOrder>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    kOrder.Add(new ReadKwickOrder(dr));
                }
            }
            return kOrder;
        }

        public List<kwickOrder> GetReqOrder(string id, string val)
        {
            string query = "";

            if (val == "1")
            {
                query = "select k.isAccept,k.koId,k.cityId,k.cityName,k.areaId,k.areaName,k.Location,k.srId,k.serviceName,k.customerName,k.mobile,k.orderDate" +
                ",k.shopId,k.lat,k.longs,c.companyName,k.dName from tbl.kwickOrder k join tbl.CompanyProfile c on k.ShopId = c.Userid order by k.koId desc";
            }
            else if (id == "10")
            {
                query = "select k.isAccept,k.koId,k.cityId,k.cityName,k.areaId,k.areaName,k.Location,k.srId,k.serviceName,k.customerName,k.mobile,k.orderDate" +
                     ",k.shopId,k.lat,k.longs,c.companyName,k.dName from tbl.kwickOrder k join tbl.CompanyProfile c on k.ShopId = c.Userid " +
                     " where k.did in (select profileid from tbl.driver where userid='" + val + "') order by k.koId desc";
            }
            else
            {
                query = "select k.isAccept,k.koId,k.cityId,k.cityName,k.areaId,k.areaName,k.Location,k.srId,k.serviceName,k.customerName,k.mobile,k.orderDate" +
                ",k.shopId,k.lat,k.longs,c.companyName,k.dName from tbl.kwickOrder k join tbl.CompanyProfile c on k.ShopId = c.Userid " +
                "where k.shopId='" + val + "' order by k.koId desc";
            }
            

            DataTable dt = Database.get_DataTable(query);
            List<kwickOrder> kOrder = new List<Models.kwickOrder>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    kOrder.Add(new ReadKwickOrderDt(dr));
                }
            }
            return kOrder;
        }

        public List<kwickOrder> GetReqOrderHistory(string id, string val,string cid)
        {
            string query = "";

            if (val == "0")
            {
                query = "select k.koId,k.cityId,k.cityName,k.areaId,k.areaName,k.Location,k.srId,k.serviceName,k.customerName,k.mobile,k.orderDate" +
                ",k.shopId,k.lat,k.longs,c.companyName,k.dName,k.isAccept from tbl.kwickOrder k join tbl.CompanyProfile c on k.ShopId = c.Userid " +
                "where k.Userid='" + cid + "' order by k.koId desc";
            }

            DataTable dt = Database.get_DataTable(query);
            List<kwickOrder> kOrder = new List<Models.kwickOrder>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    kOrder.Add(new ReadKwickOrderDt(dr));
                }
            }
            return kOrder;
        }

        public List<kwickOrder> GetDriverReqOrder(string id, string val, string cid,string did)
        {
            string query = "";

            if (val == "0")
            {
                query = "select k.koId,k.cityId,k.cityName,k.areaId,k.areaName,k.Location,k.srId,k.serviceName,k.customerName,k.mobile,k.orderDate" +
                ",k.shopId,k.lat,k.longs,c.companyName,k.dName,k.isAccept from tbl.kwickOrder k join tbl.CompanyProfile c on k.ShopId = c.Userid " +
                "where k.did='" + did + "' order by k.koId desc";
            }

            DataTable dt = Database.get_DataTable(query);
            List<kwickOrder> kOrder = new List<Models.kwickOrder>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    kOrder.Add(new ReadKwickOrderDt(dr));
                }
            }
            return kOrder;
        }


        public string Post([FromBody] CreatekwickOrder values)
        {
            try
            {
                string query_profile = "", query_login = "";
                string queryShop = "select shopId from tbl.area where areaId='" + values.areaId + "'";
                DataSet dsShopId = Database.get_DataSet(queryShop);
                if (dsShopId.Tables[0].Rows.Count > 0)
                {
                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                    string cname = values.customerName;
                    cname = cname.Replace("`", "");

                    //Login Table
                                        
                    query_login = "insert into tbl.login (username,password,name,email,role,status) values ('" + values.mobile
                        + "','pass@123','" + cname.ToString() + "','0','2','1')";

                    // Profile Table
                    query_profile = "declare @Userid bigint select @Userid=IDENT_CURRENT('tbl.login')";

                    query_profile += "insert into tbl.Profile(dtmAdd,userId,name,mobile,city,upLineId,Location) values " +
                        "('" + DateTime.Now.ToString() + "',@Userid,'" + cname.ToString() + "','" + values.mobile
                        + "','" + values.cityName + "','0','" + values.Location + "')";

                    //Kwick Order Table
                    string query = "declare @Userid bigint select @Userid=IDENT_CURRENT('tbl.login') insert into tbl.kwickOrder(cityId,cityName,areaId,areaName,Location,srId,serviceName,customerName,mobile," +
                        "orderDate,shopId,lat,longs,UserId) values ('" + values.cityId + "','" + values.cityName + "','" + values.areaId + "','" + values.areaName
                        + "','" + values.Location + "','" + values.srId + "','" + values.serviceName + "','" + values.customerName + "','" + values.mobile
                        + "','" + dateTime + "','" + dsShopId.Tables[0].Rows[0]["shopId"].ToString() + "','" + values.lat
                        + "','" + values.longs + "', @Userid)";
                    string ress = Database.Execute_Transaction(query_login, query_profile, query);
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
    }
}
