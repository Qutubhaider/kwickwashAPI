using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using APIKwickWash.Models;
using System.Web.Cors;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace APIKwickWash.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class dashboardController : ApiController
    {

        public IEnumerable<dashboard> Get()
        {
            string query = "select * from tbl.ttlUserDashboard";
            DataTable dt = Database.get_DataTable(query);
            List<dashboard> dashboard = new List<Models.dashboard>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dashboard.Add(new ReadDashboard(dr));
                }
            }
            return dashboard;
        }

        public IEnumerable<dashboard> Get(int id)
        {
            string query = "select * from tbl.ttlUserDashboard where userid='" + id + "'";
            DataTable dt = Database.get_DataTable(query);
            List<dashboard> dashboard = new List<Models.dashboard>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                int TotalCustomer = 0, ToatlService = 0, TotalProduct = 0, TotalOrder = 0, TotalPendingOrder = 0, TotalCompletedOrder = 0, TotalDriver = 0;
                double TotalRevenue = 0.0, TotalCollection = 0.0, TotalOutstanding = 0.0;
                string query_counter = "select count(*) as TotalCustomer from tbl.Profile where uplineid='" + id + "'" +
               " select count(*)as ToatlService from tbl.myservice where userid='" + id + "' " +
               " select count(*)as TotalProduct from tbl.myproduct where userid='" + id + "' " +
               " select count(*)as TotalOrder from tbl.orders where suserid='" + id + "' " +
               " select count(*)as TotalPendingOrder from tbl.orders where deliverystatus='' and suserid='" + id + "'" +
               " select count(*)as TotalCompletedOrder from tbl.orders where deliverystatus='Delivered' and suserid='" + id + "'" +
               " select sum(ttlPayableAmount)as TotalRevenue from tbl.orders where suserid='" + id + "'" +
               " select sum(ttlPayableAmount)as TotalCollection from tbl.orders where status='Paid' and suserid='" + id + "'" +
               " select sum(ttlPayableAmount)as TotalOutstanding  from tbl.orders where status='unpaid' and suserid='" + id + "'" +
               " select count(*)as TotalDriver from tbl.driver where uplineid='" + id + "'";
                DataSet dscounter = Database.get_DataSet(query_counter);
                if (dscounter.Tables[0].Rows.Count > 0)
                {
                    if (dscounter.Tables[0].Rows[0]["TotalCustomer"] != DBNull.Value)
                    {
                        TotalCustomer = Convert.ToInt32(dscounter.Tables[0].Rows[0]["TotalCustomer"]);
                    }
                }
                if (dscounter.Tables[1].Rows.Count > 0)
                {
                    if (dscounter.Tables[1].Rows[0]["ToatlService"] != DBNull.Value)
                    {
                        ToatlService = Convert.ToInt32(dscounter.Tables[1].Rows[0]["ToatlService"]);
                    }
                }
                if (dscounter.Tables[2].Rows.Count > 0)
                {
                    if (dscounter.Tables[2].Rows[0]["TotalProduct"] != DBNull.Value)
                    {
                        TotalProduct = Convert.ToInt32(dscounter.Tables[2].Rows[0]["TotalProduct"]);
                    }
                }
                if (dscounter.Tables[3].Rows.Count > 0)
                {
                    if (dscounter.Tables[3].Rows[0]["TotalOrder"] != DBNull.Value)
                    {
                        TotalOrder = Convert.ToInt32(dscounter.Tables[3].Rows[0]["TotalOrder"]);
                    }
                }
                if (dscounter.Tables[4].Rows.Count > 0)
                {
                    if (dscounter.Tables[4].Rows[0]["TotalPendingOrder"] != DBNull.Value)
                    {
                        TotalPendingOrder = Convert.ToInt32(dscounter.Tables[4].Rows[0]["TotalPendingOrder"]);
                    }
                }
                if (dscounter.Tables[5].Rows.Count > 0)
                {
                    if (dscounter.Tables[5].Rows[0]["TotalCompletedOrder"] != DBNull.Value)
                    {
                        TotalCompletedOrder = Convert.ToInt32(dscounter.Tables[5].Rows[0]["TotalCompletedOrder"]);
                    }
                }
                if (dscounter.Tables[6].Rows.Count > 0)
                {
                    if (dscounter.Tables[6].Rows[0]["TotalRevenue"] != DBNull.Value)
                    {
                        TotalRevenue = Convert.ToDouble(dscounter.Tables[6].Rows[0]["TotalRevenue"]);
                    }
                }
                if (dscounter.Tables[7].Rows.Count > 0)
                {
                    if (dscounter.Tables[7].Rows[0]["TotalCollection"] != DBNull.Value)
                    {
                        TotalCollection = Convert.ToDouble(dscounter.Tables[7].Rows[0]["TotalCollection"]);
                    }
                }
                if (dscounter.Tables[8].Rows.Count > 0)
                {
                    if (dscounter.Tables[8].Rows[0]["TotalOutstanding"] != DBNull.Value)
                    {
                        TotalOutstanding = Convert.ToDouble(dscounter.Tables[8].Rows[0]["TotalOutstanding"]);
                    }
                }
                if (dscounter.Tables[9].Rows.Count > 0)
                {
                    if (dscounter.Tables[9].Rows[0]["TotalDriver"] != DBNull.Value)
                    {
                        TotalDriver = Convert.ToInt32(dscounter.Tables[9].Rows[0]["TotalDriver"]);
                    }
                }
                string query_update = "update tbl.ttlUserDashboard set ttlCustomer='" + TotalCustomer + "', ttlService='" + ToatlService
                    + "', ttlProduct='" + TotalProduct + "',ttlOrders='" + TotalOrder + "',ttlOrderPending='" + TotalPendingOrder
                    + "',ttlOrderCompleted='" + TotalCompletedOrder + "',ttlPayments='" + TotalRevenue + "',ttlPaymentsPending='" + TotalCollection
                    + "',ttlPaymentsCompleted='" + TotalOutstanding + "',ttlDeliveryCompleted='" + TotalCompletedOrder
                    + "',ttlDeliveryPending='" + TotalPendingOrder + "',ttlDriver='" + TotalDriver + "' where userid='" + id + "'";
                int res = Database.Execute(query_update);
                foreach (DataRow dr in dt.Rows)
                {
                    dashboard.Add(new ReadDashboard(dr));
                }
            }
            return dashboard;
        }

        public MonthDashboard GetMonthData(int id, int month, int year)
        {
            string query = "SELECT COUNT(*) as TotalCustomer from tbl.Profile where uplineid='" + id + "' AND DATEPART(MONTH,DTMADD)='" + month + "' AND DATEPART(YEAR,DTMADD)='" + year + "'" +
                " SELECT COUNT(*) AS TotalOrder FROM tbl.Orders WHERE SUSERID='" + id + "' AND DATEPART(MONTH, ORDERDATE)='" + month + "' AND DATEPART(YEAR, ORDERDATE)='" + year + "' " +
                " SELECT COUNT(*)as TotalPendingOrder from tbl.orders where deliverystatus = '' and SUSERID='" + id + "' AND DATEPART(MONTH, ORDERDATE)='" + month + "' AND DATEPART(YEAR, ORDERDATE)='" + year + "' " +
                " SELECT COUNT(*)as TotalCompletedOrder from tbl.orders where deliverystatus = 'Delivered' and SUSERID='" + id + "' AND DATEPART(MONTH, ORDERDATE)='" + month + "' AND DATEPART(YEAR, ORDERDATE)='" + year + "' " +
                " SELECT SUM(ttlPayableAmount)as TotalRevenue from tbl.orders where SUSERID='" + id + "' AND DATEPART(MONTH, ORDERDATE)='" + month + "' AND DATEPART(YEAR, ORDERDATE)='" + year + "' " +
                " SELECT SUM(ttlPayableAmount)as TotalCollection from tbl.orders where status='Paid' and SUSERID='" + id + "' AND DATEPART(MONTH, ORDERDATE)='" + month + "' AND DATEPART(YEAR, ORDERDATE)='" + year + "' " +
                " SELECT SUM(ttlPayableAmount)as TotalOutstanding  from tbl.orders where status='unpaid' and SUSERID='" + id + "' AND DATEPART(MONTH, ORDERDATE)='" + month + "' AND DATEPART(YEAR, ORDERDATE)='" + year + "'";
            DataSet ds = Database.get_DataSet(query);
            MonthDashboard loMonthDashboard = new MonthDashboard();

            #region TotalCustomer
            if (ds.Tables[0].Rows.Count > 0)
            {
                loMonthDashboard.TotalCustomer = (int)ds.Tables[0].Rows[0]["TotalCustomer"];
            }
            else
            {
                loMonthDashboard.TotalCustomer = 0;
            }
            #endregion

            #region TotalOrder
            if (ds.Tables[1].Rows.Count > 0)
            {
                loMonthDashboard.TotalOrder = (int)ds.Tables[1].Rows[0]["TotalOrder"];
            }
            else
            {
                loMonthDashboard.TotalOrder = 0;
            }
            #endregion

            #region TotalPendingOrder
            if (ds.Tables[2].Rows.Count > 0)
            {
                loMonthDashboard.TotalPendingOrder = (int)ds.Tables[2].Rows[0]["TotalPendingOrder"];
            }
            else
            {
                loMonthDashboard.TotalPendingOrder = 0;
            }
            #endregion

            #region TotalCompletedOrder
            if (ds.Tables[3].Rows.Count > 0)
            {
                loMonthDashboard.TotalCompletedOrder = (int)ds.Tables[3].Rows[0]["TotalCompletedOrder"];
            }
            else
            {
                loMonthDashboard.TotalCompletedOrder = 0;
            }
            #endregion

            #region TotalRevenue
            if (ds.Tables[4].Rows.Count > 0)
            {
                loMonthDashboard.TotalRevenue = Math.Round(Convert.ToDecimal(ds.Tables[4].Rows[0]["TotalRevenue"]), 2);
            }
            else
            {
                loMonthDashboard.TotalRevenue = 0;
            }
            #endregion

            #region TotalCollection
            if (ds.Tables[5].Rows.Count > 0)
            {
                if (ds.Tables[5].Rows[0]["TotalCollection"] != DBNull.Value)
                {
                    loMonthDashboard.TotalCollection = Math.Round(Convert.ToDecimal(ds.Tables[5].Rows[0]["TotalCollection"]), 2);
                }
                else
                {
                    loMonthDashboard.TotalCollection = 0;
                }
            }
            else
            {
                loMonthDashboard.TotalCollection = 0;
            }
            #endregion

            #region TotalOutstanding
            if (ds.Tables[6].Rows.Count > 0)
            {
                loMonthDashboard.TotalOutstanding = Math.Round(Convert.ToDecimal(ds.Tables[6].Rows[0]["TotalOutstanding"]), 2);
            }
            else
            {
                loMonthDashboard.TotalOutstanding = 0;
            }
            #endregion

            return loMonthDashboard;
        }
    }
}
