using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace APIKwickWash.Models
{
    public class dashboard
    {
        public string dashboardId { get; set; }
        public string ttlCustomer { get; set; }
        public string ttlUser { get; set; }
        public string ttlDriver { get; set; }
        public string ttlService { get; set; }
        public string ttlProduct { get; set; }
        public string ttlOrders { get; set; }
        public string ttlOrderPending { get; set; }
        public string ttlOrderCompleted { get; set; }
        public string ttlProcess { get; set; }
        public string ttlProcessPending { get; set; }
        public string ttlProcessCompleted { get; set; }
        public string ttlInvoice { get; set; }
        public string ttlInvoicePending { get; set; }
        public string ttlInvoiceCompleted { get; set; }
        public string ttlPayments { get; set; }
        public string ttlPaymentsPending { get; set; }
        public string ttlPaymentsCompleted { get; set; }
        public string ttlDelivery { get; set; }
        public string ttlDeliveryPending { get; set; }
        public string ttlDeliveryCompleted { get; set; }
    }

    public class ReadDashboard:dashboard
    {
        public ReadDashboard(DataRow dr)
        {
            dashboardId = dr["dashboardId"].ToString();
            ttlCustomer = dr["ttlCustomer"].ToString();
            ttlUser = dr["ttlUser"].ToString();
            ttlDriver = dr["ttlDriver"].ToString();
            ttlService = dr["ttlService"].ToString();
            ttlProduct = dr["ttlProduct"].ToString();
            ttlOrders = dr["ttlOrders"].ToString();
            ttlOrderPending = dr["ttlOrderPending"].ToString();
            ttlOrderCompleted = dr["ttlOrderCompleted"].ToString();
            ttlProcess = dr["ttlProcess"].ToString();
            ttlProcessPending = dr["ttlProcessPending"].ToString();
            ttlProcessCompleted = dr["ttlProcessCompleted"].ToString();
            ttlInvoice = dr["ttlInvoice"].ToString();
            ttlInvoicePending = dr["ttlInvoicePending"].ToString();
            ttlInvoiceCompleted = dr["ttlInvoiceCompleted"].ToString();
            ttlPayments = dr["ttlPayments"].ToString();
            ttlPaymentsPending = dr["ttlPaymentsPending"].ToString();
            ttlPaymentsCompleted = dr["ttlPaymentsCompleted"].ToString();
            ttlDelivery = dr["ttlDelivery"].ToString();
            ttlDeliveryPending = dr["ttlDeliveryPending"].ToString();
            ttlDeliveryCompleted = dr["ttlDeliveryCompleted"].ToString();
        }
    }
}