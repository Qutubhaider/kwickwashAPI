using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace APIKwickWash.Models
{
    public class coupon
    {
        public string cpId { get; set; }
        public string couponCode { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string srId { get; set; }
        public string serviceName { get; set; }
        public string proId { get; set; }
        public string productName { get; set; }
        public string customerType { get; set; }
        public string validNoOfTime { get; set; }
        public string couponValue { get; set; }
        public string loginUserid { get; set; }
    }

    public class CreateCoupon:coupon
    {

    }

    public class ReadCoupon:coupon
    {
        public ReadCoupon(DataRow dr)
        {
            cpId = dr["cpId"].ToString();
            couponCode = dr["couponCode"].ToString();
            DateTime sdate = Convert.ToDateTime(dr["startDate"]);
            startDate = sdate.ToString("dd-MMM-yyyy").ToString();
            DateTime eDate = Convert.ToDateTime(dr["endDate"]);
            endDate = eDate.ToString("dd-MMM-yyyy").ToString();
            srId = dr["srId"].ToString();
            serviceName = dr["serviceName"].ToString();
            proId = dr["proId"].ToString();
            productName = dr["productName"].ToString();
            customerType = dr["customerType"].ToString();
            validNoOfTime = dr["validNoOfTime"].ToString();
            couponValue = dr["couponValue"].ToString();
            loginUserid = dr["loginUserid"].ToString();
        }
    }
}