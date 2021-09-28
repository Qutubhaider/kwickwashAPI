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
    public class couponController : ApiController
    {
        // GET: coupon
        public IEnumerable<coupon> Get(int id)
        {
            string query = "";
            if (id==0)
            {
                query = "select * from tbl.coupon where  startDate<='" + DateTime.Now.ToString() + "' and endDate>='" + DateTime.Now.ToString() + "'";
                DataTable dt = Database.get_DataTable(query);
                List<coupon> coupon = new List<Models.coupon>(dt.Rows.Count);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        coupon.Add(new ReadCoupon(dr));
                    }
                }
                return coupon;
            }
            else
            {
                query = "select * from tbl.coupon where loginUserid='0' and startDate<='" + DateTime.Now.ToString() + "' and endDate>='" + DateTime.Now.ToString() + "'";
                DataTable dt = Database.get_DataTable(query);
                List<coupon> coupon = new List<Models.coupon>(dt.Rows.Count);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        coupon.Add(new ReadCoupon(dr));
                    }
                }

                string query1 = "select * from tbl.coupon where loginUserid='" + id + "' and startDate<='" + DateTime.Now.ToString() + "' and endDate>='" + DateTime.Now.ToString() + "'";
                DataSet ds = Database.get_DataSet(query1);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {
                        coupon.Add(new ReadCoupon(dr1));
                    }
                }
                return coupon;
            }
           
        }


        public List<coupon> GetCoupon(string couponcode,string val)
        {
            string query = "";
            //query = "select * from tbl.coupon where couponCode='" + couponcode + "'";
            query = "select * from tbl.coupon where couponCode='"+couponcode+"' and startDate<='"+DateTime.Now.ToString()+ "' and endDate>='" + DateTime.Now.ToString() + "'";
            DataTable dt = Database.get_DataTable(query);
            List<coupon> coupon = new List<Models.coupon>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    coupon.Add(new ReadCoupon(dr));
                }
            }
            return coupon;
        }

        public string Post([FromBody]CreateCoupon data)
        {
            try
            {
                string query = "insert into tbl.coupon (couponCode,startDate,endDate,srId,serviceName,proId,productName,customerType," +
                    "validNoOfTime,couponValue,loginUserid) values ('" + data.couponCode + "','" + data.startDate + "','" + data.endDate
                    + "','" + data.srId + "','" + data.serviceName + "','" + data.proId + "','" + data.productName + "','" + data.customerType
                    + "','" + data.validNoOfTime + "','" + data.couponValue + "','" + data.loginUserid + "')";
                int res = Database.Execute(query);               
                if (res == 1)
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