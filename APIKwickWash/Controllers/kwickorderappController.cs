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
    public class kwickorderappController : ApiController
    {
        public string Post([FromBody] Createkwickorderapp values)
        {
            try
            {
                string query_profile = "", query_login = "";
                string queryShop = "select shopId,areaName from tbl.area where areaId='" + values.areaId + "'" +
                    " select cityName from tbl.city where cityId='" + values.cityId + "' " +
                    " select serviceName from tbl.myservice where srId='" + values.srId + "'";
                DataSet dsShopId = Database.get_DataSet(queryShop);
                if (dsShopId.Tables[0].Rows.Count > 0)
                {
                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                   
                    //Kwick Order throught Mobile Apps Table
                    string query = "insert into tbl.kwickOrder(cityId,cityName,areaId,areaName,Location,srId,serviceName,customerName," +
                        "mobile,orderDate,shopId,lat,longs,UserId) values ('" + values.cityId + "','" + dsShopId.Tables[1].Rows[0]["cityName"].ToString()
                        + "','" + values.areaId + "','" + dsShopId.Tables[0].Rows[0]["areaName"].ToString()
                        + "','" + values.Location + "','" + values.srId + "','" + dsShopId.Tables[2].Rows[0]["serviceName"].ToString()
                        + "','" + values.customerName + "','" + values.mobile + "','" + dateTime
                        + "','" + dsShopId.Tables[0].Rows[0]["shopId"].ToString() + "','" + values.lat + "','" + values.longs + "', '" + values.userId + "')";
                    int ress = Database.Execute(query);
                    if (ress == 1)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
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
