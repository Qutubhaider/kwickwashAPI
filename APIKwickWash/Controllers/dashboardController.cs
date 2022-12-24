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

            int TotalCustomer = 0, ToatlService = 0, TotalProduct = 0, TotalOrder = 0, TotalPendingOrder = 0, TotalCompletedOrder = 0, TotalDriver = 0;
            int Booked = 0, InProcess = 0, ReadyForDelivery = 0, DeliveredUnpaid = 0, DeliveredPaid = 0;
            double TotalRevenue = 0.0, TotalCollection = 0.0, TotalOutstanding = 0.0;
            double BookedAmount = 0.0, InProcessAmount = 0.0, ReadyForDeliveryAmount = 0.0, DeliveredUnpaidAmount = 0.0, DeliveredPaidAmount = 0.0, WALLETBALANCE = 0.0;
            string query_counter = string.Empty;
            if (id == 1)
            {
                query_counter = "select count(*) as TotalCustomer from tbl.Profile" +
                       " select count(*)as ToatlService from tbl.service" +
                       " select count(*)as TotalProduct from tbl.product " +
                       " select count(*)as TotalOrder from tbl.orders ";
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
                string query_update = "update tbl.ttlUserDashboard set ttlCustomer='" + TotalCustomer + "', ttlService='" + ToatlService
                    + "', ttlProduct='" + TotalProduct + "',ttlOrders='" + TotalOrder + "' where userid='" + id + "'";
                int res = Database.Execute(query_update);
            }
            else
            {
                query_counter = "select count(*) as TotalCustomer from tbl.Profile where uplineid='" + id + "'" +
               " select count(*)as ToatlService from tbl.myservice where userid='" + id + "' " +
               " select count(*)as TotalProduct from tbl.myproduct where userid='" + id + "' " +
               " select count(*)as TotalOrder from tbl.orders where suserid='" + id + "' " +
               " select count(*)as TotalPendingOrder from tbl.orders where deliverystatus!='Delivered' and suserid='" + id + "'" +
               " select count(*)as TotalCompletedOrder from tbl.orders where deliverystatus='Delivered' and suserid='" + id + "'" +
               " select sum(PATABLEAMOUNT)as TotalRevenue from tbl.orders where suserid='" + id + "'" +
               " select sum(PATABLEAMOUNT)as TotalCollection from tbl.orders where status='Paid' and suserid='" + id + "'" +
               " select sum(PATABLEAMOUNT)as TotalOutstanding  from tbl.orders where status='unpaid' and suserid='" + id + "'" +
               " select count(*)as TotalDriver from tbl.driver where uplineid='" + id + "'" +
               " SELECT COUNT(*)as Booked FROM tbl.Orders WHERE deliveryStatus='' and suserid='" + id + "' " +
               " SELECT COUNT(*)as InProcess FROM tbl.Orders WHERE deliveryStatus='InProcess' AND suserid='" + id + "' " +
               " SELECT COUNT(*)as ReadyForDelivery FROM tbl.Orders WHERE deliveryStatus='ReadyForDelivery' AND suserid='" + id + "' " +
               " SELECT COUNT(*)as DeliveredUnpaid FROM tbl.Orders WHERE deliveryStatus='Delivered' AND [Status]='unpaid' AND suserid='" + id + "' " +
               " SELECT COUNT(*)as DeliveredPaid FROM tbl.Orders WHERE deliveryStatus='Delivered' AND [Status]='Paid' AND suserid='" + id + "' " +
               " select sum(PATABLEAMOUNT)as BookedAmount from tbl.orders where deliveryStatus='' and suserid='" + id + "'" +
               " select sum(PATABLEAMOUNT)as InProcessAmount from tbl.orders where deliveryStatus='InProcess' and suserid='" + id + "'" +
               " select sum(PATABLEAMOUNT)as ReadyForDeliveryAmount from tbl.orders where deliveryStatus='ReadyForDelivery' and suserid='" + id + "'" +
               " select sum(PATABLEAMOUNT)as DeliveredUnpaidAmount from tbl.orders where deliveryStatus='Delivered' AND [Status]='unpaid' and suserid='" + id + "'" +
               " select sum(PATABLEAMOUNT)as DeliveredPaidAmount from tbl.orders where deliveryStatus='Delivered' AND [Status]='Paid' and suserid='" + id + "'" +
               " SELECT SUM(balance) AS WALLETBALANCE FROM tbl.Profile WHERE upLineId='" + id + "' AND balance>0";
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
                if (dscounter.Tables[10].Rows.Count > 0)
                {
                    if (dscounter.Tables[10].Rows[0]["Booked"] != DBNull.Value)
                        Booked = Convert.ToInt32(dscounter.Tables[10].Rows[0]["Booked"]);
                }
                if (dscounter.Tables[11].Rows.Count > 0)
                {
                    if (dscounter.Tables[11].Rows[0]["InProcess"] != DBNull.Value)
                        InProcess = Convert.ToInt32(dscounter.Tables[11].Rows[0]["InProcess"]);
                }
                if (dscounter.Tables[12].Rows.Count > 0)
                {
                    if (dscounter.Tables[12].Rows[0]["ReadyForDelivery"] != DBNull.Value)
                        ReadyForDelivery = Convert.ToInt32(dscounter.Tables[12].Rows[0]["ReadyForDelivery"]);
                }
                if (dscounter.Tables[13].Rows.Count > 0)
                {
                    if (dscounter.Tables[13].Rows[0]["DeliveredUnpaid"] != DBNull.Value)
                        DeliveredUnpaid = Convert.ToInt32(dscounter.Tables[13].Rows[0]["DeliveredUnpaid"]);
                }
                if (dscounter.Tables[14].Rows.Count > 0)
                {
                    if (dscounter.Tables[14].Rows[0]["DeliveredPaid"] != DBNull.Value)
                        DeliveredPaid = Convert.ToInt32(dscounter.Tables[14].Rows[0]["DeliveredPaid"]);
                }
                if (dscounter.Tables[15].Rows.Count > 0)
                {
                    if (dscounter.Tables[15].Rows[0]["BookedAmount"] != DBNull.Value)
                        BookedAmount = Convert.ToDouble(dscounter.Tables[15].Rows[0]["BookedAmount"]);
                }
                if (dscounter.Tables[16].Rows.Count > 0)
                {
                    if (dscounter.Tables[16].Rows[0]["InProcessAmount"] != DBNull.Value)
                        InProcessAmount = Convert.ToDouble(dscounter.Tables[16].Rows[0]["InProcessAmount"]);
                }
                if (dscounter.Tables[17].Rows.Count > 0)
                {
                    if (dscounter.Tables[17].Rows[0]["ReadyForDeliveryAmount"] != DBNull.Value)
                        ReadyForDeliveryAmount = Convert.ToDouble(dscounter.Tables[17].Rows[0]["ReadyForDeliveryAmount"]);
                }
                if (dscounter.Tables[18].Rows.Count > 0)
                {
                    if (dscounter.Tables[18].Rows[0]["DeliveredUnpaidAmount"] != DBNull.Value)
                        DeliveredUnpaidAmount = Convert.ToDouble(dscounter.Tables[18].Rows[0]["DeliveredUnpaidAmount"]);
                }
                if (dscounter.Tables[19].Rows.Count > 0)
                {
                    if (dscounter.Tables[19].Rows[0]["DeliveredPaidAmount"] != DBNull.Value)
                        DeliveredPaidAmount = Convert.ToDouble(dscounter.Tables[19].Rows[0]["DeliveredPaidAmount"]);
                }
                if (dscounter.Tables[20].Rows.Count > 0)
                {
                    if (dscounter.Tables[20].Rows[0]["WALLETBALANCE"] != DBNull.Value)
                        WALLETBALANCE = Convert.ToDouble(dscounter.Tables[20].Rows[0]["WALLETBALANCE"]);
                }
                string query_update = "update tbl.ttlUserDashboard set ttlCustomer='" + TotalCustomer + "', ttlService='" + ToatlService
                    + "', ttlProduct='" + TotalProduct + "',ttlOrders='" + TotalOrder + "',ttlOrderPending='" + TotalPendingOrder
                    + "', ttlOrderCompleted='" + TotalCompletedOrder + "',ttlPayments='" + TotalRevenue + "',ttlPaymentsPending='" + TotalCollection
                    + "', ttlPaymentsCompleted='" + TotalOutstanding + "',ttlDeliveryCompleted='" + TotalCompletedOrder
                    + "', ttlDeliveryPending='" + TotalPendingOrder + "',ttlDriver='" + TotalDriver + "', Booked='" + Booked
                    + "', InProcess='" + InProcess + "', ReadyForDelivery='" + ReadyForDelivery + "', DeliveredUnpaid='" + DeliveredUnpaid
                    + "', DeliveredPaid='" + DeliveredPaid + "', BookedAmount='" + BookedAmount + "', InProcessAmount='" + InProcessAmount
                    + "', ReadyForDeliveryAmount='" + ReadyForDeliveryAmount + "', DeliveredUnpaidAmount='" + DeliveredUnpaidAmount
                    + "', DeliveredPaidAmount='" + DeliveredPaidAmount + "', WALLETBALANCE='" + WALLETBALANCE + "' where userid='" + id + "'";
                int res = Database.Execute(query_update);
            }
            string query = "select * from tbl.ttlUserDashboard where userid='" + id + "'";
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

        public IEnumerable<dashboard> GetAdmin(int id, string fdate, string tdate)
        {

            int TotalCustomer = 0, ToatlService = 0, TotalProduct = 0, TotalOrder = 0, TotalPendingOrder = 0, TotalCompletedOrder = 0, TotalDriver = 0;
            int Booked = 0, InProcess = 0, ReadyForDelivery = 0, DeliveredUnpaid = 0, DeliveredPaid = 0, TOTALReferral = 0, TOTALVENDOR = 0, TOTALSHOP = 0;
            double TotalRevenue = 0.0, TotalCollection = 0.0, TotalOutstanding = 0.0;
            double BookedAmount = 0.0, InProcessAmount = 0.0, ReadyForDeliveryAmount = 0.0, DeliveredUnpaidAmount = 0.0, DeliveredPaidAmount = 0.0, WALLETBALANCE = 0.0;
            string query_counter = string.Empty;

            if (!string.IsNullOrEmpty(fdate) && !string.IsNullOrEmpty(tdate))
            {
                query_counter = "select count(*) as TotalCustomer from tbl.Profile where convert(date,dtmAdd) between '" + fdate + "' and '" + tdate + "'" +
              " select count(*)as ToatlService from tbl.service " +
              " select count(*)as TotalProduct from tbl.product " +
              " select count(*)as TotalOrder from tbl.orders where convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select count(*)as TotalPendingOrder from tbl.orders where deliverystatus!='Delivered' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select count(*)as TotalCompletedOrder from tbl.orders where deliverystatus='Delivered' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as TotalRevenue from tbl.orders where convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as TotalCollection from tbl.orders where status='Paid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as TotalOutstanding  from tbl.orders where status='unpaid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select count(*)as TotalDriver from tbl.driver where convert(date,dtmAdd) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as Booked FROM tbl.Orders WHERE deliveryStatus='' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as InProcess FROM tbl.Orders WHERE deliveryStatus='InProcess' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as ReadyForDelivery FROM tbl.Orders WHERE deliveryStatus='ReadyForDelivery' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as DeliveredUnpaid FROM tbl.Orders WHERE deliveryStatus='Delivered' AND [Status]='unpaid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as DeliveredPaid FROM tbl.Orders WHERE deliveryStatus='Delivered' AND [Status]='Paid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as BookedAmount from tbl.orders where deliveryStatus='' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as InProcessAmount from tbl.orders where deliveryStatus='InProcess' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as ReadyForDeliveryAmount from tbl.orders where deliveryStatus='ReadyForDelivery' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as DeliveredUnpaidAmount from tbl.orders where deliveryStatus='Delivered' AND [Status]='unpaid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as DeliveredPaidAmount from tbl.orders where deliveryStatus='Delivered' AND [Status]='Paid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "'  " +
              " SELECT SUM(balance) AS WALLETBALANCE FROM tbl.Profile WHERE  balance>0 " +
              " select Count(*)AS TOTALVENDOR  from tbl.CompanyProfile WHERE inRole=0  " +
              " select Count(*)AS TOTALReferral  from tbl.CompanyProfile WHERE inRole=1 " +
              " select Count(*)AS TOTALSHOP  from tbl.CompanyProfile WHERE inRole=2  ";
            }
            else
            {
                query_counter = "select count(*) as TotalCustomer from tbl.Profile" +
               " select count(*)as ToatlService from tbl.service " +
               " select count(*)as TotalProduct from tbl.product " +
               " select count(*)as TotalOrder from tbl.orders " +
               " select count(*)as TotalPendingOrder from tbl.orders where deliverystatus!='Delivered' " +
               " select count(*)as TotalCompletedOrder from tbl.orders where deliverystatus='Delivered' " +
               " select sum(PATABLEAMOUNT)as TotalRevenue from tbl.orders " +
               " select sum(PATABLEAMOUNT)as TotalCollection from tbl.orders where status='Paid' " +
               " select sum(PATABLEAMOUNT)as TotalOutstanding  from tbl.orders where status='unpaid' " +
               " select count(*)as TotalDriver from tbl.driver " +
               " SELECT COUNT(*)as Booked FROM tbl.Orders WHERE deliveryStatus='' " +
               " SELECT COUNT(*)as InProcess FROM tbl.Orders WHERE deliveryStatus='InProcess' " +
               " SELECT COUNT(*)as ReadyForDelivery FROM tbl.Orders WHERE deliveryStatus='ReadyForDelivery' " +
               " SELECT COUNT(*)as DeliveredUnpaid FROM tbl.Orders WHERE deliveryStatus='Delivered' AND [Status]='unpaid' " +
               " SELECT COUNT(*)as DeliveredPaid FROM tbl.Orders WHERE deliveryStatus='Delivered' AND [Status]='Paid' " +
               " select sum(PATABLEAMOUNT)as BookedAmount from tbl.orders where deliveryStatus='' " +
               " select sum(PATABLEAMOUNT)as InProcessAmount from tbl.orders where deliveryStatus='InProcess' " +
               " select sum(PATABLEAMOUNT)as ReadyForDeliveryAmount from tbl.orders where deliveryStatus='ReadyForDelivery' " +
               " select sum(PATABLEAMOUNT)as DeliveredUnpaidAmount from tbl.orders where deliveryStatus='Delivered' AND [Status]='unpaid' " +
               " select sum(PATABLEAMOUNT)as DeliveredPaidAmount from tbl.orders where deliveryStatus='Delivered' AND [Status]='Paid'  " +
               " SELECT SUM(balance) AS WALLETBALANCE FROM tbl.Profile WHERE  balance>0" +
               " select Count(*)AS TOTALVENDOR  from tbl.CompanyProfile WHERE inRole=0" +
               " select Count(*)AS TOTALReferral  from tbl.CompanyProfile WHERE inRole=1" +
               " select Count(*)AS TOTALSHOP  from tbl.CompanyProfile WHERE inRole=2";
            }
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
            if (dscounter.Tables[10].Rows.Count > 0)
            {
                if (dscounter.Tables[10].Rows[0]["Booked"] != DBNull.Value)
                    Booked = Convert.ToInt32(dscounter.Tables[10].Rows[0]["Booked"]);
            }
            if (dscounter.Tables[11].Rows.Count > 0)
            {
                if (dscounter.Tables[11].Rows[0]["InProcess"] != DBNull.Value)
                    InProcess = Convert.ToInt32(dscounter.Tables[11].Rows[0]["InProcess"]);
            }
            if (dscounter.Tables[12].Rows.Count > 0)
            {
                if (dscounter.Tables[12].Rows[0]["ReadyForDelivery"] != DBNull.Value)
                    ReadyForDelivery = Convert.ToInt32(dscounter.Tables[12].Rows[0]["ReadyForDelivery"]);
            }
            if (dscounter.Tables[13].Rows.Count > 0)
            {
                if (dscounter.Tables[13].Rows[0]["DeliveredUnpaid"] != DBNull.Value)
                    DeliveredUnpaid = Convert.ToInt32(dscounter.Tables[13].Rows[0]["DeliveredUnpaid"]);
            }
            if (dscounter.Tables[14].Rows.Count > 0)
            {
                if (dscounter.Tables[14].Rows[0]["DeliveredPaid"] != DBNull.Value)
                    DeliveredPaid = Convert.ToInt32(dscounter.Tables[14].Rows[0]["DeliveredPaid"]);
            }
            if (dscounter.Tables[15].Rows.Count > 0)
            {
                if (dscounter.Tables[15].Rows[0]["BookedAmount"] != DBNull.Value)
                    BookedAmount = Convert.ToDouble(dscounter.Tables[15].Rows[0]["BookedAmount"]);
            }
            if (dscounter.Tables[16].Rows.Count > 0)
            {
                if (dscounter.Tables[16].Rows[0]["InProcessAmount"] != DBNull.Value)
                    InProcessAmount = Convert.ToDouble(dscounter.Tables[16].Rows[0]["InProcessAmount"]);
            }
            if (dscounter.Tables[17].Rows.Count > 0)
            {
                if (dscounter.Tables[17].Rows[0]["ReadyForDeliveryAmount"] != DBNull.Value)
                    ReadyForDeliveryAmount = Convert.ToDouble(dscounter.Tables[17].Rows[0]["ReadyForDeliveryAmount"]);
            }
            if (dscounter.Tables[18].Rows.Count > 0)
            {
                if (dscounter.Tables[18].Rows[0]["DeliveredUnpaidAmount"] != DBNull.Value)
                    DeliveredUnpaidAmount = Convert.ToDouble(dscounter.Tables[18].Rows[0]["DeliveredUnpaidAmount"]);
            }
            if (dscounter.Tables[19].Rows.Count > 0)
            {
                if (dscounter.Tables[19].Rows[0]["DeliveredPaidAmount"] != DBNull.Value)
                    DeliveredPaidAmount = Convert.ToDouble(dscounter.Tables[19].Rows[0]["DeliveredPaidAmount"]);
            }
            if (dscounter.Tables[20].Rows.Count > 0)
            {
                if (dscounter.Tables[20].Rows[0]["WALLETBALANCE"] != DBNull.Value)
                    WALLETBALANCE = Convert.ToDouble(dscounter.Tables[20].Rows[0]["WALLETBALANCE"]);
            }
            if (dscounter.Tables[21].Rows.Count > 0)
            {
                if (dscounter.Tables[21].Rows[0]["TOTALVENDOR"] != DBNull.Value)
                {
                    TOTALVENDOR = Convert.ToInt32(dscounter.Tables[21].Rows[0]["TOTALVENDOR"]);
                }
            }
            if (dscounter.Tables[22].Rows.Count > 0)
            {
                if (dscounter.Tables[22].Rows[0]["TOTALReferral"] != DBNull.Value)
                {
                    TOTALReferral = Convert.ToInt32(dscounter.Tables[22].Rows[0]["TOTALReferral"]);
                }
            }
            if (dscounter.Tables[23].Rows.Count > 0)
            {
                if (dscounter.Tables[23].Rows[0]["TOTALSHOP"] != DBNull.Value)
                {
                    TOTALSHOP = Convert.ToInt32(dscounter.Tables[23].Rows[0]["TOTALSHOP"]);
                }
            }
            string query_update = "update tbl.ttlUserDashboard set ttlCustomer='" + TotalCustomer + "', ttlService='" + ToatlService
                + "', ttlProduct='" + TotalProduct + "',ttlOrders='" + TotalOrder + "',ttlOrderPending='" + TotalPendingOrder
                + "', ttlOrderCompleted='" + TotalCompletedOrder + "',ttlPayments='" + TotalRevenue + "',ttlPaymentsPending='" + TotalCollection
                + "', ttlPaymentsCompleted='" + TotalOutstanding + "',ttlDeliveryCompleted='" + TotalCompletedOrder
                + "', ttlDeliveryPending='" + TotalPendingOrder + "',ttlDriver='" + TotalDriver + "', Booked='" + Booked
                + "', InProcess='" + InProcess + "', ReadyForDelivery='" + ReadyForDelivery + "', DeliveredUnpaid='" + DeliveredUnpaid
                + "', DeliveredPaid='" + DeliveredPaid + "', BookedAmount='" + BookedAmount + "', InProcessAmount='" + InProcessAmount
                + "', ReadyForDeliveryAmount='" + ReadyForDeliveryAmount + "', DeliveredUnpaidAmount='" + DeliveredUnpaidAmount
                + "', DeliveredPaidAmount='" + DeliveredPaidAmount + "', WALLETBALANCE='" + WALLETBALANCE + "', TOTALReferral='" + TOTALReferral
                + "', TOTALVENDOR='" + TOTALVENDOR + "',TOTALSHOP='" + TOTALSHOP + "' where userid='" + id + "'";
            int res = Database.Execute(query_update);

            string query = "select * from tbl.ttlUserDashboard where userid='" + id + "'";
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

        public IEnumerable<dashboard> GetShop(int id, string fdate, string tdate, string name)
        {

            int TotalCustomer = 0, ToatlService = 0, TotalProduct = 0, TotalOrder = 0, TotalPendingOrder = 0, TotalCompletedOrder = 0, TotalDriver = 0;
            int Booked = 0, InProcess = 0, ReadyForDelivery = 0, DeliveredUnpaid = 0, DeliveredPaid = 0, TOTALReferral = 0, TOTALVENDOR = 0, TOTALSHOP = 0;
            double TotalRevenue = 0.0, TotalCollection = 0.0, TotalOutstanding = 0.0;
            double BookedAmount = 0.0, InProcessAmount = 0.0, ReadyForDeliveryAmount = 0.0, DeliveredUnpaidAmount = 0.0, DeliveredPaidAmount = 0.0, WALLETBALANCE = 0.0;
            string query_counter = string.Empty;

            if (!string.IsNullOrEmpty(fdate) && !string.IsNullOrEmpty(tdate))
            {
                query_counter = "select count(*) as TotalCustomer from tbl.Profile where uplineid='" + id + "' and convert(date,dtmAdd) between '" + fdate + "' and '" + tdate + "'" +
              " select count(*)as ToatlService from tbl.myservice where userid='" + id + "'" +
              " select count(*)as TotalProduct from tbl.myproduct where userid='" + id + "'" +
              " select count(*)as TotalOrder from tbl.orders where suserid='" + id + "' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select count(*)as TotalPendingOrder from tbl.orders where suserid='" + id + "' and deliverystatus!='Delivered' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select count(*)as TotalCompletedOrder from tbl.orders where suserid='" + id + "' and deliverystatus='Delivered' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as TotalRevenue from tbl.orders where suserid='" + id + "' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as TotalCollection from tbl.orders where suserid='" + id + "' and status='Paid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as TotalOutstanding  from tbl.orders where suserid='" + id + "' and status='unpaid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select count(*)as TotalDriver from tbl.driver where uplineid='" + id + "' and convert(date,dtmAdd) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as Booked FROM tbl.Orders WHERE suserid='" + id + "' and deliveryStatus='' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as InProcess FROM tbl.Orders WHERE suserid='" + id + "' and deliveryStatus='InProcess' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as ReadyForDelivery FROM tbl.Orders  WHERE suserid='" + id + "' and deliveryStatus='ReadyForDelivery' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as DeliveredUnpaid FROM tbl.Orders WHERE suserid='" + id + "' and deliveryStatus='Delivered' AND [Status]='unpaid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as DeliveredPaid FROM tbl.Orders WHERE suserid='" + id + "' and deliveryStatus='Delivered' AND [Status]='Paid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as BookedAmount from tbl.orders  where suserid='" + id + "' and deliveryStatus='' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as InProcessAmount from tbl.orders where suserid='" + id + "' and deliveryStatus='InProcess' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as ReadyForDeliveryAmount from tbl.orders where suserid='" + id + "' and deliveryStatus='ReadyForDelivery' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as DeliveredUnpaidAmount from tbl.orders where suserid='" + id + "' and deliveryStatus='Delivered' AND [Status]='unpaid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as DeliveredPaidAmount from tbl.orders where suserid='" + id + "' and deliveryStatus='Delivered' AND [Status]='Paid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "'  " +
              " SELECT SUM(balance) AS WALLETBALANCE FROM tbl.Profile WHERE uplineid='" + id + "' and  balance>0" +
              " select Count(*)AS TOTALVENDOR  from tbl.CompanyProfile WHERE inRole=0" +
              " select Count(*)AS TOTALReferral  from tbl.CompanyProfile WHERE inRole=1" +
              " select Count(*)AS TOTALSHOP  from tbl.CompanyProfile WHERE inRole=2";
            }
            else
            {
                query_counter = "select count(*) as TotalCustomer from tbl.Profile where uplineid='" + id + "'" +
               " select count(*)as ToatlService from tbl.myservice where userid='" + id + "' " +
               " select count(*)as TotalProduct from tbl.myproduct where userid='" + id + "' " +
               " select count(*)as TotalOrder from tbl.orders where suserid='" + id + "' " +
               " select count(*)as TotalPendingOrder from tbl.orders  where suserid='" + id + "' and deliverystatus!='Delivered' " +
               " select count(*)as TotalCompletedOrder from tbl.orders where suserid='" + id + "' and deliverystatus='Delivered' " +
               " select sum(PATABLEAMOUNT)as TotalRevenue from tbl.orders where suserid='" + id + "' " +
               " select sum(PATABLEAMOUNT)as TotalCollection from tbl.orders where suserid='" + id + "' and status='Paid' " +
               " select sum(PATABLEAMOUNT)as TotalOutstanding  from tbl.orders where suserid='" + id + "' and status='unpaid' " +
               " select count(*)as TotalDriver from tbl.driver where uplineid='" + id + "' " +
               " SELECT COUNT(*)as Booked FROM tbl.Orders WHERE suserid='" + id + "' and deliveryStatus='' " +
               " SELECT COUNT(*)as InProcess FROM tbl.Orders WHERE suserid='" + id + "' and deliveryStatus='InProcess' " +
               " SELECT COUNT(*)as ReadyForDelivery FROM tbl.Orders WHERE suserid='" + id + "' and deliveryStatus='ReadyForDelivery' " +
               " SELECT COUNT(*)as DeliveredUnpaid FROM tbl.Orders WHERE suserid='" + id + "' and deliveryStatus='Delivered' AND [Status]='unpaid' " +
               " SELECT COUNT(*)as DeliveredPaid FROM tbl.Orders WHERE suserid='" + id + "' and deliveryStatus='Delivered' AND [Status]='Paid' " +
               " select sum(PATABLEAMOUNT)as BookedAmount from tbl.orders where suserid='" + id + "' and deliveryStatus='' " +
               " select sum(PATABLEAMOUNT)as InProcessAmount from tbl.orders where suserid='" + id + "' and deliveryStatus='InProcess' " +
               " select sum(PATABLEAMOUNT)as ReadyForDeliveryAmount from tbl.orders where suserid='" + id + "' and deliveryStatus='ReadyForDelivery' " +
               " select sum(PATABLEAMOUNT)as DeliveredUnpaidAmount from tbl.orders where suserid='" + id + "' and deliveryStatus='Delivered' AND [Status]='unpaid' " +
               " select sum(PATABLEAMOUNT)as DeliveredPaidAmount from tbl.orders where suserid='" + id + "' and deliveryStatus='Delivered' AND [Status]='Paid'  " +
               " SELECT SUM(balance) AS WALLETBALANCE FROM tbl.Profile WHERE uplineid='" + id + "' and  balance>0" +
               " select Count(*)AS TOTALVENDOR  from tbl.CompanyProfile WHERE inRole=0" +
               " select Count(*)AS TOTALReferral  from tbl.CompanyProfile WHERE inRole=1" +
               " select Count(*)AS TOTALSHOP  from tbl.CompanyProfile WHERE inRole=2";
            }
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
            if (dscounter.Tables[10].Rows.Count > 0)
            {
                if (dscounter.Tables[10].Rows[0]["Booked"] != DBNull.Value)
                    Booked = Convert.ToInt32(dscounter.Tables[10].Rows[0]["Booked"]);
            }
            if (dscounter.Tables[11].Rows.Count > 0)
            {
                if (dscounter.Tables[11].Rows[0]["InProcess"] != DBNull.Value)
                    InProcess = Convert.ToInt32(dscounter.Tables[11].Rows[0]["InProcess"]);
            }
            if (dscounter.Tables[12].Rows.Count > 0)
            {
                if (dscounter.Tables[12].Rows[0]["ReadyForDelivery"] != DBNull.Value)
                    ReadyForDelivery = Convert.ToInt32(dscounter.Tables[12].Rows[0]["ReadyForDelivery"]);
            }
            if (dscounter.Tables[13].Rows.Count > 0)
            {
                if (dscounter.Tables[13].Rows[0]["DeliveredUnpaid"] != DBNull.Value)
                    DeliveredUnpaid = Convert.ToInt32(dscounter.Tables[13].Rows[0]["DeliveredUnpaid"]);
            }
            if (dscounter.Tables[14].Rows.Count > 0)
            {
                if (dscounter.Tables[14].Rows[0]["DeliveredPaid"] != DBNull.Value)
                    DeliveredPaid = Convert.ToInt32(dscounter.Tables[14].Rows[0]["DeliveredPaid"]);
            }
            if (dscounter.Tables[15].Rows.Count > 0)
            {
                if (dscounter.Tables[15].Rows[0]["BookedAmount"] != DBNull.Value)
                    BookedAmount = Convert.ToDouble(dscounter.Tables[15].Rows[0]["BookedAmount"]);
            }
            if (dscounter.Tables[16].Rows.Count > 0)
            {
                if (dscounter.Tables[16].Rows[0]["InProcessAmount"] != DBNull.Value)
                    InProcessAmount = Convert.ToDouble(dscounter.Tables[16].Rows[0]["InProcessAmount"]);
            }
            if (dscounter.Tables[17].Rows.Count > 0)
            {
                if (dscounter.Tables[17].Rows[0]["ReadyForDeliveryAmount"] != DBNull.Value)
                    ReadyForDeliveryAmount = Convert.ToDouble(dscounter.Tables[17].Rows[0]["ReadyForDeliveryAmount"]);
            }
            if (dscounter.Tables[18].Rows.Count > 0)
            {
                if (dscounter.Tables[18].Rows[0]["DeliveredUnpaidAmount"] != DBNull.Value)
                    DeliveredUnpaidAmount = Convert.ToDouble(dscounter.Tables[18].Rows[0]["DeliveredUnpaidAmount"]);
            }
            if (dscounter.Tables[19].Rows.Count > 0)
            {
                if (dscounter.Tables[19].Rows[0]["DeliveredPaidAmount"] != DBNull.Value)
                    DeliveredPaidAmount = Convert.ToDouble(dscounter.Tables[19].Rows[0]["DeliveredPaidAmount"]);
            }
            if (dscounter.Tables[20].Rows.Count > 0)
            {
                if (dscounter.Tables[20].Rows[0]["WALLETBALANCE"] != DBNull.Value)
                    WALLETBALANCE = Convert.ToDouble(dscounter.Tables[20].Rows[0]["WALLETBALANCE"]);
            }
            if (dscounter.Tables[21].Rows.Count > 0)
            {
                if (dscounter.Tables[21].Rows[0]["TOTALVENDOR"] != DBNull.Value)
                {
                    TOTALVENDOR = Convert.ToInt32(dscounter.Tables[21].Rows[0]["TOTALVENDOR"]);
                }
            }
            if (dscounter.Tables[22].Rows.Count > 0)
            {
                if (dscounter.Tables[22].Rows[0]["TOTALReferral"] != DBNull.Value)
                {
                    TOTALReferral = Convert.ToInt32(dscounter.Tables[22].Rows[0]["TOTALReferral"]);
                }
            }
            if (dscounter.Tables[23].Rows.Count > 0)
            {
                if (dscounter.Tables[23].Rows[0]["TOTALSHOP"] != DBNull.Value)
                {
                    TOTALSHOP = Convert.ToInt32(dscounter.Tables[23].Rows[0]["TOTALSHOP"]);
                }
            }
            string query_update = "update tbl.ttlUserDashboard set ttlCustomer='" + TotalCustomer + "', ttlService='" + ToatlService
                + "', ttlProduct='" + TotalProduct + "',ttlOrders='" + TotalOrder + "',ttlOrderPending='" + TotalPendingOrder
                + "', ttlOrderCompleted='" + TotalCompletedOrder + "',ttlPayments='" + TotalRevenue + "',ttlPaymentsPending='" + TotalCollection
                + "', ttlPaymentsCompleted='" + TotalOutstanding + "',ttlDeliveryCompleted='" + TotalCompletedOrder
                + "', ttlDeliveryPending='" + TotalPendingOrder + "',ttlDriver='" + TotalDriver + "', Booked='" + Booked
                + "', InProcess='" + InProcess + "', ReadyForDelivery='" + ReadyForDelivery + "', DeliveredUnpaid='" + DeliveredUnpaid
                + "', DeliveredPaid='" + DeliveredPaid + "', BookedAmount='" + BookedAmount + "', InProcessAmount='" + InProcessAmount
                + "', ReadyForDeliveryAmount='" + ReadyForDeliveryAmount + "', DeliveredUnpaidAmount='" + DeliveredUnpaidAmount
                + "', DeliveredPaidAmount='" + DeliveredPaidAmount + "', WALLETBALANCE='" + WALLETBALANCE + "', TOTALReferral='" + TOTALReferral
                + "', TOTALVENDOR='" + TOTALVENDOR + "',TOTALSHOP='" + TOTALSHOP + "' where userid='" + id + "'";
            int res = Database.Execute(query_update);

            string query = "select * from tbl.ttlUserDashboard where userid='" + id + "'";
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

        public VendorShopCount GetVendorShopCount(int rid, string fdate, string tdate, int status, int statuss)
        {
            string query_counter = string.Empty;
            if (!string.IsNullOrEmpty(fdate) && !string.IsNullOrEmpty(tdate))
            {
                query_counter = "select count(*) as TotalShop  from tbl.CompanyProfile where userId in(select userId from tbl.CompanyProfile " +
                " where convert(date,dtmAdd) between '" + fdate + "' and '" + tdate + "' and " +
                " inVendorId in(select userId from tbl.CompanyProfile where  inReferalId='" + rid + "'))" +
                " select count(*) AS TotalVendor from tbl.CompanyProfile where convert(date,dtmAdd) between '" + fdate + "' and '" + tdate + "' and inReferalId='" + rid + "'";
            }
            else
            {
                query_counter = "select count(*) as TotalShop  from tbl.CompanyProfile where userId in(select userId from tbl.CompanyProfile " +
                 " where inVendorId in(select userId from tbl.CompanyProfile where  inReferalId='" + rid + "'))" +
                 " select count(*) AS TotalVendor from tbl.CompanyProfile " +
                 " where  inReferalId ='" + rid + "'";
            }

            DataSet dscounter = Database.get_DataSet(query_counter);

            VendorShopCount loVendorShopCount = new VendorShopCount();

            if (dscounter.Tables[0].Rows.Count > 0)
            {
                loVendorShopCount.TotalShop = Convert.ToInt32(dscounter.Tables[0].Rows[0]["TotalShop"]);
            }
            else
            {
                loVendorShopCount.TotalShop = 0;
            }

            if (dscounter.Tables[1].Rows.Count > 0)
            {
                loVendorShopCount.TotalVendor = Convert.ToInt32(dscounter.Tables[1].Rows[0]["TotalVendor"]);
            }
            else
            {
                loVendorShopCount.TotalVendor = 0;
            }

            return loVendorShopCount;
        }


        public dashboard GetVendorShop(int vid, string fdate, string tdate, int status)
        {
            string query_counter = string.Empty;
            if (!string.IsNullOrEmpty(fdate) && !string.IsNullOrEmpty(tdate))
            {
                query_counter = "select count(*) as TotalCustomer from tbl.Profile where upLineId in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and convert(date,dtmAdd) between '" + fdate + "' and '" + tdate + "'" +
              " select count(*)as ToatlService from tbl.service " +
              " select count(*)as TotalProduct from tbl.product " +
              " select count(*)as TotalOrder from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select count(*)as TotalPendingOrder from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliverystatus!='Delivered' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select count(*)as TotalCompletedOrder from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliverystatus='Delivered' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as TotalRevenue from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as TotalCollection from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and status='Paid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as TotalOutstanding  from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and status='unpaid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select count(*)as TotalDriver from tbl.driver where convert(date,dtmAdd) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as Booked FROM tbl.Orders WHERE suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as InProcess FROM tbl.Orders WHERE suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='InProcess' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as ReadyForDelivery FROM tbl.Orders WHERE suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='ReadyForDelivery' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as DeliveredUnpaid FROM tbl.Orders WHERE suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='Delivered' AND [Status]='unpaid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " SELECT COUNT(*)as DeliveredPaid FROM tbl.Orders WHERE suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='Delivered' AND [Status]='Paid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as BookedAmount from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as InProcessAmount from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='InProcess' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as ReadyForDeliveryAmount from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='ReadyForDelivery' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as DeliveredUnpaidAmount from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='Delivered' AND [Status]='unpaid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "' " +
              " select sum(PATABLEAMOUNT)as DeliveredPaidAmount from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='Delivered' AND [Status]='Paid' and convert(date,orderDate) between '" + fdate + "' and '" + tdate + "'  " +
              " SELECT SUM(balance) AS WALLETBALANCE FROM tbl.Profile WHERE upLineId in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and  balance>0 " +
              " select Count(*)AS TOTALVENDOR  from tbl.CompanyProfile WHERE inVendorId='" + vid + "' and inRole=0  " +
              " select Count(*)AS TOTALReferral  from tbl.CompanyProfile WHERE inVendorId='" + vid + "' and inRole=1 " +
              " select Count(*)AS TOTALSHOP  from tbl.CompanyProfile WHERE inVendorId='" + vid + "' and inRole=2  ";
            }
            else
            {
                query_counter = "select count(*) as TotalCustomer from tbl.Profile where upLineId in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "')" +
               " select count(*)as ToatlService from tbl.service " +
               " select count(*)as TotalProduct from tbl.product " +
               " select count(*)as TotalOrder from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') " +
               " select count(*)as TotalPendingOrder from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliverystatus!='Delivered' " +
               " select count(*)as TotalCompletedOrder from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliverystatus='Delivered' " +
               " select sum(PATABLEAMOUNT)as TotalRevenue from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "')" +
               " select sum(PATABLEAMOUNT)as TotalCollection from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and status='Paid' " +
               " select sum(PATABLEAMOUNT)as TotalOutstanding  from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and status='unpaid' " +
               " select count(*)as TotalDriver from tbl.driver " +
               " SELECT COUNT(*)as Booked FROM tbl.Orders WHERE suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='' " +
               " SELECT COUNT(*)as InProcess FROM tbl.Orders WHERE suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='InProcess' " +
               " SELECT COUNT(*)as ReadyForDelivery FROM tbl.Orders WHERE suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='ReadyForDelivery' " +
               " SELECT COUNT(*)as DeliveredUnpaid FROM tbl.Orders WHERE suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='Delivered' AND [Status]='unpaid' " +
               " SELECT COUNT(*)as DeliveredPaid FROM tbl.Orders WHERE suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='Delivered' AND [Status]='Paid' " +
               " select sum(PATABLEAMOUNT)as BookedAmount from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='' " +
               " select sum(PATABLEAMOUNT)as InProcessAmount from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='InProcess' " +
               " select sum(PATABLEAMOUNT)as ReadyForDeliveryAmount from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='ReadyForDelivery' " +
               " select sum(PATABLEAMOUNT)as DeliveredUnpaidAmount from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='Delivered' AND [Status]='unpaid' " +
               " select sum(PATABLEAMOUNT)as DeliveredPaidAmount from tbl.orders where suserid in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and deliveryStatus='Delivered' AND [Status]='Paid'  " +
               " SELECT SUM(balance) AS WALLETBALANCE FROM tbl.Profile WHERE upLineId in (select userid from tbl.CompanyProfile where inVendorId='" + vid + "') and balance>0" +
               " select Count(*)AS TOTALVENDOR  from tbl.CompanyProfile WHERE inVendorId='" + vid + "' and inRole=0" +
               " select Count(*)AS TOTALReferral  from tbl.CompanyProfile WHERE inVendorId='" + vid + "' and inRole=1" +
               " select Count(*)AS TOTALSHOP  from tbl.CompanyProfile WHERE inVendorId='" + vid + "' and inRole=2";
            }
            DataSet dscounter = Database.get_DataSet(query_counter);
            dashboard loDashboard = new dashboard();
            if (dscounter.Tables[0].Rows.Count > 0)
            {
                if (dscounter.Tables[0].Rows[0]["TotalCustomer"] != DBNull.Value)
                {
                    loDashboard.ttlCustomer = dscounter.Tables[0].Rows[0]["TotalCustomer"].ToString();
                }
            }
            if (dscounter.Tables[1].Rows.Count > 0)
            {
                if (dscounter.Tables[1].Rows[0]["ToatlService"] != DBNull.Value)
                {
                    loDashboard.ttlService = dscounter.Tables[1].Rows[0]["ToatlService"].ToString();
                }
            }
            if (dscounter.Tables[2].Rows.Count > 0)
            {
                if (dscounter.Tables[2].Rows[0]["TotalProduct"] != DBNull.Value)
                {
                    loDashboard.ttlProduct = dscounter.Tables[2].Rows[0]["TotalProduct"].ToString();
                }
            }
            if (dscounter.Tables[3].Rows.Count > 0)
            {
                if (dscounter.Tables[3].Rows[0]["TotalOrder"] != DBNull.Value)
                {
                    loDashboard.ttlOrders = dscounter.Tables[3].Rows[0]["TotalOrder"].ToString();
                }
            }
            if (dscounter.Tables[4].Rows.Count > 0)
            {
                if (dscounter.Tables[4].Rows[0]["TotalPendingOrder"] != DBNull.Value)
                {
                    loDashboard.ttlOrderPending = dscounter.Tables[4].Rows[0]["TotalPendingOrder"].ToString();
                }
            }
            if (dscounter.Tables[5].Rows.Count > 0)
            {
                if (dscounter.Tables[5].Rows[0]["TotalCompletedOrder"] != DBNull.Value)
                {
                    loDashboard.ttlOrderCompleted = dscounter.Tables[5].Rows[0]["TotalCompletedOrder"].ToString();
                }
            }
            if (dscounter.Tables[6].Rows.Count > 0)
            {
                if (dscounter.Tables[6].Rows[0]["TotalRevenue"] != DBNull.Value)
                {
                    loDashboard.ttlPayments = dscounter.Tables[6].Rows[0]["TotalRevenue"].ToString();
                }
            }
            if (dscounter.Tables[7].Rows.Count > 0)
            {
                if (dscounter.Tables[7].Rows[0]["TotalCollection"] != DBNull.Value)
                {
                    loDashboard.ttlPaymentsPending = dscounter.Tables[7].Rows[0]["TotalCollection"].ToString();
                }
            }
            if (dscounter.Tables[8].Rows.Count > 0)
            {
                if (dscounter.Tables[8].Rows[0]["TotalOutstanding"] != DBNull.Value)
                {
                    loDashboard.ttlPaymentsCompleted = dscounter.Tables[8].Rows[0]["TotalOutstanding"].ToString();
                }
            }
            if (dscounter.Tables[9].Rows.Count > 0)
            {
                if (dscounter.Tables[9].Rows[0]["TotalDriver"] != DBNull.Value)
                {
                    loDashboard.ttlDriver = dscounter.Tables[9].Rows[0]["TotalDriver"].ToString();
                }
            }
            if (dscounter.Tables[10].Rows.Count > 0)
            {
                if (dscounter.Tables[10].Rows[0]["Booked"] != DBNull.Value)
                    loDashboard.Booked = Convert.ToInt32(dscounter.Tables[10].Rows[0]["Booked"]);
            }
            if (dscounter.Tables[11].Rows.Count > 0)
            {
                if (dscounter.Tables[11].Rows[0]["InProcess"] != DBNull.Value)
                    loDashboard.InProcess = Convert.ToInt32(dscounter.Tables[11].Rows[0]["InProcess"]);
            }
            if (dscounter.Tables[12].Rows.Count > 0)
            {
                if (dscounter.Tables[12].Rows[0]["ReadyForDelivery"] != DBNull.Value)
                    loDashboard.ReadyForDelivery = Convert.ToInt32(dscounter.Tables[12].Rows[0]["ReadyForDelivery"]);
            }
            if (dscounter.Tables[13].Rows.Count > 0)
            {
                if (dscounter.Tables[13].Rows[0]["DeliveredUnpaid"] != DBNull.Value)
                    loDashboard.DeliveredUnpaid = Convert.ToInt32(dscounter.Tables[13].Rows[0]["DeliveredUnpaid"]);
            }
            if (dscounter.Tables[14].Rows.Count > 0)
            {
                if (dscounter.Tables[14].Rows[0]["DeliveredPaid"] != DBNull.Value)
                    loDashboard.DeliveredPaid = Convert.ToInt32(dscounter.Tables[14].Rows[0]["DeliveredPaid"]);
            }
            if (dscounter.Tables[15].Rows.Count > 0)
            {
                if (dscounter.Tables[15].Rows[0]["BookedAmount"] != DBNull.Value)
                    loDashboard.BookedAmount = Convert.ToDecimal(dscounter.Tables[15].Rows[0]["BookedAmount"]);
            }
            if (dscounter.Tables[16].Rows.Count > 0)
            {
                if (dscounter.Tables[16].Rows[0]["InProcessAmount"] != DBNull.Value)
                    loDashboard.InProcessAmount = Convert.ToDecimal(dscounter.Tables[16].Rows[0]["InProcessAmount"]);
            }
            if (dscounter.Tables[17].Rows.Count > 0)
            {
                if (dscounter.Tables[17].Rows[0]["ReadyForDeliveryAmount"] != DBNull.Value)
                    loDashboard.ReadyForDeliveryAmount = Convert.ToDecimal(dscounter.Tables[17].Rows[0]["ReadyForDeliveryAmount"]);
            }
            if (dscounter.Tables[18].Rows.Count > 0)
            {
                if (dscounter.Tables[18].Rows[0]["DeliveredUnpaidAmount"] != DBNull.Value)
                    loDashboard.DeliveredUnpaidAmount = Convert.ToDecimal(dscounter.Tables[18].Rows[0]["DeliveredUnpaidAmount"]);
            }
            if (dscounter.Tables[19].Rows.Count > 0)
            {
                if (dscounter.Tables[19].Rows[0]["DeliveredPaidAmount"] != DBNull.Value)
                    loDashboard.DeliveredPaidAmount = Convert.ToDecimal(dscounter.Tables[19].Rows[0]["DeliveredPaidAmount"]);
            }
            if (dscounter.Tables[20].Rows.Count > 0)
            {
                if (dscounter.Tables[20].Rows[0]["WALLETBALANCE"] != DBNull.Value)
                    loDashboard.WalletBalance = Convert.ToDecimal(dscounter.Tables[20].Rows[0]["WALLETBALANCE"]);
            }
            if (dscounter.Tables[21].Rows.Count > 0)
            {
                if (dscounter.Tables[21].Rows[0]["TOTALVENDOR"] != DBNull.Value)
                {
                    loDashboard.ttlVendor = Convert.ToInt32(dscounter.Tables[21].Rows[0]["TOTALVENDOR"]);
                }
            }
            if (dscounter.Tables[22].Rows.Count > 0)
            {
                if (dscounter.Tables[22].Rows[0]["TOTALReferral"] != DBNull.Value)
                {
                    loDashboard.ttlReferral = Convert.ToInt32(dscounter.Tables[22].Rows[0]["TOTALReferral"]);
                }
            }
            if (dscounter.Tables[23].Rows.Count > 0)
            {
                if (dscounter.Tables[23].Rows[0]["TOTALSHOP"] != DBNull.Value)
                {
                    loDashboard.ttlShop = Convert.ToInt32(dscounter.Tables[23].Rows[0]["TOTALSHOP"]);
                }
            }
            return loDashboard;
        }
    }
}
