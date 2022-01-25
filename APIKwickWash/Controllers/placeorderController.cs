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
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace APIKwickWash.Controllers
{
    public class placeorderController : ApiController
    {
        // GET: placeorder

        public IEnumerable<placeorder> Get(int id)
        {
            string query = "select * from tbl.OrderItems where orderId='" + id.ToString() + "'";
            DataTable dt = Database.get_DataTable(query);
            List<placeorder> placeorder = new List<Models.placeorder>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    placeorder.Add(new Readplaceorder(dr));
                }
            }
            return placeorder;
        }

        public List<placeorder> GetDataOrder(int id,string status)
        {
            string query = "select oi.orderItemId,oi.OrderId,oi.CUserId,oi.SUserId,oi.InvoiceNo,oi.SrId,oi.ServiceName,oi.proId,oi.ProductName,oi.Unit,oi.price," +
                " oi.orderQty,oi.totalQty,p.productCode,s.serviceDescription from tbl.OrderItems oi join tbl.product p on oi.proId = p.proId join tbl.service s" +
                " on oi.SrId = s.SrId where oi.orderId='" + id.ToString() + "'";
            DataTable dt = Database.get_DataTable(query);
            List<placeorder> placeorder = new List<Models.placeorder>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    placeorder.Add(new Readplaceorder1(dr));
                }
            }
            return placeorder;
        }

        public static int GenerateRandomInt(Random rnd)
        {
            return rnd.Next();
        }
        public string Post([FromBody]Createplaceorder data)
        {
            System.Text.StringBuilder lslog = new System.Text.StringBuilder();
            try
            {
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                string query_Exc = " declare @orderId bigint select @orderId=IDENT_CURRENT('tbl.[Orders]')";
                double ttlQty = 0, ttlAmount = 0.0, couponValue = 0.0, discountAmt = 0.0, payableAmt = 0.0;
                string query_Order = "", query_delete_Cart = "", res = "", queryAddon = "", queryCustomerLedger = "";
                Random rnd = new Random();
                string invoiceno = GenerateRandomInt(rnd).ToString();
                string query_Check = "select * from tbl.cart where CUserid='" + data.CUserid + "' and SUserid='" + data.SUserid + "'";
                DataSet dsGet = Database.get_DataSet(query_Check);
                if (dsGet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsGet.Tables[0].Rows)
                    {
                        ttlAmount += Convert.ToDouble(dr["price"]) * Convert.ToDouble(dr["orderQty"]);

                        ttlQty += Convert.ToDouble(dr["totalQty"]);

                        query_Exc += "insert into tbl.OrderItems(orderId,CUserid,SUserid,invoiceNo,srId,serviceName,proId,productName,unit,price,orderQty,totalQty) values(@orderId,'" + data.CUserid
                           + "','" + data.SUserid + "','" + invoiceno + "','" + dr["srId"] + "','" + dr["serviceName"]
                           + "','" + dr["proId"] + "','" + dr["productName"] + "','" + dr["unit"] + "','" + dr["price"] + "','" + dr["orderQty"] + "','" + dr["totalQty"] + "')";
                    }

                    query_delete_Cart = "delete tbl.Cart where CUserid='" + data.CUserid + "' and SUserid='" + data.SUserid + "'";

                    // -- Discount Calculation Start
                    string query_coupon = "select * from tbl.coupon where couponCode='" + data.useCoupon + "'";
                    DataSet ds_coupon = Database.get_DataSet(query_coupon);
                    if(ds_coupon.Tables[0].Rows.Count>0)
                    {
                        couponValue = Convert.ToDouble(ds_coupon.Tables[0].Rows[0]["couponValue"]);
                    }

                    discountAmt = ttlAmount * couponValue / 100;
                    payableAmt = ttlAmount - discountAmt;
                    // -- Discount Calculation End

                    //-- Order Table Query
                    query_Order = "insert into tbl.Orders(invoiceNo,CUserid,SUserid,ttlQty,ttlAmount,ttlDiscount,ttlPayableAmount,Status,orderDate,deliveryDate,OrderType," +
                        "pickupRequest,dropRequest,pickupSlip,paymentMode,deliveryStatus,useCoupon,did,dname) values ('" + invoiceno + "','" + data.CUserid
                        + "','" + data.SUserid + "','" + ttlQty + "','" + ttlAmount + "','" + discountAmt + "','" + payableAmt + "','unpaid','" + dateTime.ToString()
                        + "','" + data.deliveryDate + "','" + data.OrderType + "','" + data.pickupRequest + "','" + data.dropRequest + "','" + data.pickupSlip
                        + "','" + data.paymentMode + "','" + data.deliveryStatus + "','" + data.useCoupon + "','" + data.did + "','" + data.dname + "')";

                    //-- Addon Table Query
                    if (data.AddonTotalPrice != "")
                    {
                        queryAddon = "  declare @orderId bigint select @orderId=IDENT_CURRENT('tbl.[Orders]')";
                        queryAddon += " insert into tbl.OrderAddOn (OrderId,ProdctName,Qty,Price,TotalPrice) values (@orderId,'" + data.AddonProduct + "','" + data.AddonQty + "','" + data.AddonPrice + "','" + data.AddonTotalPrice + "')";
                    }
                    else
                    {
                        queryAddon = "0";
                    }

                    //--Update Ledger

                    double balance = 0;
                    string query_getBalance = "select balance from tbl.profile where userid='" + data.CUserid + "'";
                    DataSet ds_balance = Database.get_DataSet(query_getBalance);
                    if (ds_balance.Tables[0].Rows.Count > 0)
                    {
                        balance = Convert.ToDouble(ds_balance.Tables[0].Rows[0]["balance"]);
                    }

                    double receivedAmt = Convert.ToDouble(data.AdvanceAmount);
                    double TotalBalace = balance + receivedAmt;
                    double AvaialbeBalance = TotalBalace - Convert.ToDouble(payableAmt);


                    queryCustomerLedger = "  declare @orderId bigint select @orderId=IDENT_CURRENT('tbl.[Orders]')";
                    queryCustomerLedger += "update tbl.Profile set Balance='" + AvaialbeBalance + "' where userId='" + data.CUserid + "'";
                    queryCustomerLedger += "INSERT INTO tblCustomerLedger (CustomerId,ShopId,OrderNo,EntryDate,Decsription,DebiteAmount,CreditAmount,Balance,PreviousBalanceAmount) VALUES " +
                        " ('" + data.CUserid + "','" + data.SUserid + "',@orderId,'" + dateTime.ToString() + "','','" + payableAmt
                        + "','" + receivedAmt + "','" + AvaialbeBalance + "','" + balance + "')";
                    if (TotalBalace >= payableAmt)
                    {
                        //queryCustomerLedger += " UPDATE tbl.Orders SET Status='Paid', paymentMode='Cash' WHERE orderId=@orderId";
                        queryCustomerLedger += " UPDATE tbl.Orders SET Status='Paid', paymentMode='Cash' WHERE CUserid='" + data.CUserid + "'";
                    }

                    //
                }
                
                if (queryAddon!="0")
                {
                    res = Database.Execute_Transaction(query_Order, query_Exc, query_delete_Cart, queryAddon, queryCustomerLedger);
                }
                else
                {
                    res = Database.Execute_Transaction(query_Order, query_Exc, query_delete_Cart, queryCustomerLedger);
                }
                if (res == "1")
                {

                    string query_get = "SELECT TOP 1 ORDERID FROM TBL.ORDERS WHERE SUSERID='" + data.SUserid + "' ORDER BY ORDERID DESC  SELECT MOBILE,NAME,flgIsSMS FROM TBL.PROFILE WHERE USERID='" + data.CUserid + "'";
                    DataSet ds_getOrderId = Database.get_DataSet(query_get);
                    if (ds_getOrderId.Tables[0].Rows.Count > 0)
                    {                      
                        if (ds_getOrderId.Tables[1].Rows[0]["flgIsSMS"].ToString() == "1" || ds_getOrderId.Tables[1].Rows[0]["flgIsSMS"].ToString() == "True")
                        {
                            string sendSMSAPI = "http://m1.sarv.com/api/v2.0/sms_campaign.php?token=705915705611ff6c4c5f9d8.90022694&user_id=63515991&route=TR&template_id=6745&sender_id=KWwash&language=EN&template=Dear+Qutub+thank+you+for+placing+order+with+KwickWash+Laundry+your+Order+No+1122+Dated+121212+Estimated+Cost+2022.+Stay+Updated+with+your+order+status+after+login+in+App+or+Website+at+www.kwickwash.in&contact_numbers=7277527789";
                            sendSMSAPI = sendSMSAPI.Replace("Qutub", ds_getOrderId.Tables[1].Rows[0]["NAME"].ToString());
                            sendSMSAPI = sendSMSAPI.Replace("1122", ds_getOrderId.Tables[0].Rows[0]["ORDERID"].ToString());
                            sendSMSAPI = sendSMSAPI.Replace("7277527789", ds_getOrderId.Tables[1].Rows[0]["MOBILE"].ToString());
                            sendSMSAPI = sendSMSAPI.Replace("2022", "Rs." + payableAmt.ToString());
                            sendSMSAPI = sendSMSAPI.Replace("121212", DateTime.Now.ToString("dd/MMM/yyyy HH:mm tt"));
                            lslog.AppendLine("SMSAPI : " + sendSMSAPI);
                            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSAPI);
                            httpWReq.Method = "POST";
                            httpWReq.ContentType = "application/x-www-form-urlencoded";
                            httpWReq.Timeout = 10000;
                            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                            StreamReader reader = new StreamReader(response.GetResponseStream());
                            string responseString = reader.ReadToEnd();
                            reader.Close();
                            response.Close();
                        }
                    }

                    int rest = Database.Dashboard("ttlOrders", "1", data.SUserid);
                    rest = Database.Dashboard("ttlOrderPending", "1", data.SUserid);
                    rest = Database.Dashboard("ttlPayments", ttlAmount.ToString(), data.SUserid);
                    rest = Database.Dashboard("ttlPaymentsPending", ttlAmount.ToString(), data.SUserid);
                    rest = Database.Dashboard("ttlProcess", "1", data.SUserid);
                    rest = Database.Dashboard("ttlProcessPending", "1", data.SUserid);
                    rest = Database.Dashboard("ttlDeliveryPending", "1", data.SUserid);
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "-1" + lslog.ToString();
            }
        }
    }
}