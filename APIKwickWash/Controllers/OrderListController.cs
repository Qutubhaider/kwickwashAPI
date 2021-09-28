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
    public class OrderListController : ApiController
    {
        // GET: OrderList
        public List<OrderList> Get(int id)
        {
            string query = "";
            if (id == 0)
            {
                query = "select ad.ProdctName,ad.Qty,ad.Price,ad.TotalPrice,c.companyName,o.SUserid,o.ttlDiscount,o.ttlPayableAmount,o.paymentMode,o.deliveryStatus,o.orderId,o.invoiceNo,o.ttlQty,o.ttlAmount," +
                    "o.Status,p.name,p.mobile,o.orderDate,o.deliveryDate,o.OrderType,o.pickupRequest,o.dropRequest,o.pickupSlip from tbl.Orders o join tbl.profile p" +
                    " on p.userid = o.cuserid join tbl.CompanyProfile c on o.SUserid = c.Userid  left join tbl.OrderAddOn ad on ad.OrderId=o.orderId order by o.orderId desc";
            }
            else
            {
                query = "select ad.ProdctName,ad.Qty,ad.Price,ad.TotalPrice,c.companyName,o.SUserid,o.ttlDiscount,o.ttlPayableAmount,o.paymentMode,o.deliveryStatus,o.orderId,o.invoiceNo,o.ttlQty,o.ttlAmount," +
                    "o.Status,p.name,p.mobile,o.orderDate,o.deliveryDate,o.OrderType,o.pickupRequest,o.dropRequest,o.pickupSlip from tbl.Orders o join tbl.profile p " +
                   "on p.userid=o.cuserid join tbl.CompanyProfile c on o.SUserid = c.Userid left join tbl.OrderAddOn ad on ad.OrderId=o.orderId where o.SUserid='" + id + "' order by o.orderId desc";
            }
            DataTable dt = Database.get_DataTable(query);
            List<OrderList> OrderList = new List<Models.OrderList>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    OrderList.Add(new ReadOrderList(dr));
                }
            }
            return OrderList;
        }

        public List<OrderList> GetData(int orderId,string status)
        {
            string query = "select ad.ProdctName,ad.Qty,ad.Price,ad.TotalPrice,c.companyName,o.SUserid,o.ttlDiscount,o.ttlPayableAmount,o.paymentMode,o.deliveryStatus,o.orderId,o.invoiceNo,o.ttlQty,o.ttlAmount," +
                "o.Status,p.name,p.mobile,o.orderDate,o.deliveryDate,o.OrderType,o.pickupRequest,o.dropRequest,o.pickupSlip from tbl.Orders o join tbl.profile p " +
                "on p.userid=o.cuserid join tbl.CompanyProfile c on o.SUserid = c.Userid left join tbl.OrderAddOn ad on ad.OrderId=o.orderId where o.orderId='" + orderId + "' order by o.orderId desc";
            DataTable dt = Database.get_DataTable(query);
            List<OrderList> OrderList = new List<Models.OrderList>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    OrderList.Add(new ReadOrderList(dr));
                }
            }
            return OrderList;
        }

        public List<OrderList> GetOrderReport(int id, string fdate, string tdate,string pstatus)
        {
            string query = "", query_where = "";
            if (tdate == "0")
            {
                if (id == 0)
                {
                    if (pstatus == "0")
                    {
                        query_where = "";
                    }
                    else
                    {
                        query_where = " where o.status='" + pstatus + "'";
                    }
                }
                else
                {
                    if (pstatus == "0")
                    {
                        query_where = " where o.SUserid='" + id + "'";
                    }
                    else
                    {
                        query_where = " where o.SUserid='" + id + "' and o.status='" + pstatus + "'";
                    }
                }
            }
            else
            {
                //string[] fd = fdate.Split('-');
                //string[] td = tdate.Split('-');

                //fdate = fd[2] + "-" + fd[1] + "-" + fd[0];
                //tdate = td[2] + "-" + td[1] + "-" + td[0];

                if (id == 0)
                {
                    if (pstatus == "0")
                    {
                        query_where = " where convert(date,o.orderDate) between '" + fdate.ToString() + "' and '" + tdate.ToString() + "'";
                    }
                    else
                    {
                        query_where = " where convert(date,o.orderDate) between '" + fdate.ToString()
                            + "' and '" + tdate.ToString() + "' and o.status='" + pstatus + "'";
                    }
                }
                else
                {
                    if (pstatus == "0")
                    {
                        query_where = " where o.SUserid='" + id + "' and convert(date,o.orderDate) between '" + fdate.ToString() + "' and '" + tdate.ToString() + "'";
                    }
                    else
                    {
                        query_where = " where o.SUserid='" + id + "' and convert(date,o.orderDate)  between '" + fdate.ToString()
                            + "' and '" + tdate.ToString() + "' and o.status='" + pstatus + "'";
                    }
                }

            }

            query = "select ad.ProdctName,ad.Qty,ad.Price,ad.TotalPrice,c.companyName,o.SUserid,o.ttlDiscount,o.ttlPayableAmount,o.paymentMode,o.deliveryStatus,o.orderId,o.invoiceNo,o.ttlQty,o.ttlAmount," +
                        "o.Status,p.name,p.mobile,o.orderDate,o.deliveryDate,o.OrderType,o.pickupRequest,o.dropRequest,o.pickupSlip from tbl.Orders o join tbl.profile p" +
                        " on p.userid = o.cuserid join tbl.CompanyProfile c on o.SUserid = c.Userid  left join tbl.OrderAddOn ad on ad.OrderId=o.orderId  " + query_where + " order by o.orderId asc";

            DataTable dt = Database.get_DataTable(query);
            List<OrderList> OrderList = new List<Models.OrderList>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    OrderList.Add(new ReadOrderList(dr));
                }
            }
            return OrderList;
        }

        public List<OrderList> GetOrderHistory(string orderId, string status, string cid)
        {
            string query = "";
            query = "select ad.ProdctName,ad.Qty,ad.Price,ad.TotalPrice,c.companyName,o.SUserid,o.ttlDiscount,o.ttlPayableAmount,o.paymentMode,o.deliveryStatus,o.orderId,o.invoiceNo,o.ttlQty,o.ttlAmount," +
            "o.Status,p.name,p.mobile,o.orderDate,o.deliveryDate,o.OrderType,o.pickupRequest,o.dropRequest,o.pickupSlip from tbl.Orders o join tbl.profile p " +
            "on p.userid=o.cuserid join tbl.CompanyProfile c on o.SUserid = c.Userid left join tbl.OrderAddOn ad on ad.OrderId=o.orderId where o.CUserid='" + cid + "' order by o.orderId desc";

            DataTable dt = Database.get_DataTable(query);
            List<OrderList> OrderList = new List<Models.OrderList>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    OrderList.Add(new ReadOrderList(dr));
                }
            }
            return OrderList;
        }

        public List<OrderList> GetCustomerOrderDetails(string filterData)
        {
            List<OrderList> orderLists = new List<OrderList>();
            try
            {
                string query = "select ad.ProdctName,ad.Qty,ad.Price,ad.TotalPrice,c.companyName,o.SUserid,o.ttlDiscount,o.ttlPayableAmount,o.paymentMode," +
                    "o.deliveryStatus,o.orderId,o.invoiceNo,o.ttlQty,o.ttlAmount,o.Status,p.name,p.mobile,o.orderDate,o.deliveryDate,o.OrderType," +
                    "o.pickupRequest,o.dropRequest,o.pickupSlip from tbl.Orders o join tbl.profile p on p.userid=o.cuserid join tbl.CompanyProfile " +
                    "c on o.SUserid = c.Userid left join tbl.OrderAddOn ad on ad.OrderId=o.orderId where o.CUserid in (select userid from )" +
                    "order by o.orderId desc";
                return orderLists;
            }
            catch(Exception ex)
            {
                return orderLists;
            }
        }
    }
}