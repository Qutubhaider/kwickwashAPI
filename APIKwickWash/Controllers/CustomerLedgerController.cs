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
using System.Data.SqlClient;
using System.Configuration;

namespace APIKwickWash.Controllers
{
    public class CustomerLedgerController : ApiController
    {
        [System.Web.Mvc.HttpPost]
        public IEnumerable<CustomerLedger> Get(int id)
        {
            List<CustomerLedger> CustomerLedgerList = new List<CustomerLedger>();

            DataSet ds = new DataSet();
            SqlConnection loCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            SqlCommand loCommand = new SqlCommand();
            loCommand.Connection = loCon;
            loCommand.CommandText = "[dbo].[getCustomerLedgerList]";
            loCommand.CommandType = CommandType.StoredProcedure;
            loCommand.Parameters.Clear();
            loCommand.Parameters.Add("@inCustomerId", SqlDbType.Int).Value = id;

            SqlDataAdapter loDataAdapter = new SqlDataAdapter(loCommand);
            loDataAdapter.Fill(ds, "OutputTable");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CustomerLedger CustomerLedger = new CustomerLedger();

                    CustomerLedger.companyName = dr["companyName"].ToString();
                    CustomerLedger.orderNo = Convert.ToInt32(dr["OrderNo"]);
                    CustomerLedger.invoiceNo = Convert.ToInt32(dr["invoiceNo"]);
                    CustomerLedger.orderType = dr["orderType"].ToString();
                    CustomerLedger.dropRequest = dr["dropRequest"].ToString();

                    DateTime dtentryDate = Convert.ToDateTime(dr["entryDate"]);
                    CustomerLedger.entryDate = dtentryDate.ToString("dd-MMM-yyyy hh:mm").ToString();

                    if (dr["debiteAmount"] != DBNull.Value)
                        CustomerLedger.debiteAmount = Convert.ToDecimal(dr["debiteAmount"]);
                    else
                        CustomerLedger.debiteAmount = 0;

                    if (dr["creditAmount"] != DBNull.Value)
                        CustomerLedger.creditAmount = Convert.ToDecimal(dr["creditAmount"]);
                    else
                        CustomerLedger.creditAmount = 0;

                    if (dr["balance"] != DBNull.Value)
                        CustomerLedger.balance = Convert.ToDecimal(dr["balance"]);
                    else
                        CustomerLedger.balance = 0;

                    if (dr["ttlorder"] != DBNull.Value)
                        CustomerLedger.ttlorder = Convert.ToDecimal(dr["ttlorder"]);
                    else
                        CustomerLedger.ttlorder = 0;

                    if (dr["ttlorderamt"] != DBNull.Value)
                        CustomerLedger.ttlorderamt = Convert.ToDecimal(dr["ttlorderamt"]);
                    else
                        CustomerLedger.ttlorderamt = 0;

                    if (dr["ttlpaidamt"] != DBNull.Value)
                        CustomerLedger.ttlpaidamt = Convert.ToDecimal(dr["ttlpaidamt"]);
                    else
                        CustomerLedger.ttlpaidamt = 0;

                    if (dr["ttlunpaidamt"] != DBNull.Value)
                        CustomerLedger.ttlunpaidamt = Convert.ToDecimal(dr["ttlunpaidamt"]);
                    else
                        CustomerLedger.ttlunpaidamt = 0;

                    CustomerLedgerList.Add(CustomerLedger);
                }
            }
            return CustomerLedgerList;
        }
    }
}