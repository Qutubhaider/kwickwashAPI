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
    public class cartController : ApiController
    {
        // GET: cart
        public List<cart> GetCustomerCart(string SUserid, string CUserid)
        {
            //string query = "select c.cartId,c.CUserid,c.SUserid,c.srid,c.serviceName,c.proId,c.productName,c.unit,c.price,c.orderQty,c.totalQty,p.proImg" +
            //    " from tbl.cart c join tbl.myproduct p on p.proid = c.proid where c.CUserid='" + CUserid + "' and c.SUserid='"+ SUserid + "'";

            string query = "select * from tbl.cart where CUserid='" + CUserid + "' and SUserid='" + SUserid + "'";
            DataTable dt = Database.get_DataTable(query);
            List<cart> cart = new List<Models.cart>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    cart.Add(new ReadCart(dr));
                }
            }
            return cart;
        }

        public string Post([FromBody]CreateCart data)
        {
            try
            {
                string query = "";
                string priceRate = "0";
                string query_Check = "select * from tbl.cart where CUserid='" + data.CUserid + "' and SUserid='" + data.SUserid + "' and proId='" + data.proId + "'";
                DataSet ds_check = Database.get_DataSet(query_Check);
                if (ds_check.Tables[0].Rows.Count > 0)
                {
                    if (ds_check.Tables[0].Rows[0]["unit"].ToString() == "Kg")
                    {
                        query = "update tbl.cart set orderQty='" + data.orderQty + "',totalQty='" + data.totalQty + "' where CUserid='" + data.CUserid + "' and SUserid='" + data.SUserid
                                + "' and proId='" + data.proId + "'";
                    }
                    else
                    {
                        query = "update tbl.cart set orderQty='" + data.orderQty + "',totalQty='" + data.orderQty + "' where CUserid='" + data.CUserid + "' and SUserid='" + data.SUserid
                                + "' and proId='" + data.proId + "'";
                    }
                }
                else
                {

                    string queryPro = "select * from tbl.myproduct where proId='" + data.proId + "' and Userid='" + data.SUserid + "'";
                    DataSet dsGet = Database.get_DataSet(queryPro);
                    if (dsGet.Tables[0].Rows.Count > 0)
                    {
                        if (data.pricetype == "Drop Off")
                        {
                            priceRate = dsGet.Tables[0].Rows[0]["myPrice"].ToString();
                        }
                        else
                        {
                            priceRate = dsGet.Tables[0].Rows[0]["myPrice1"].ToString();
                        }
                        query = "insert into tbl.cart(CUserid,SUserid,srId,serviceName,proId,productName,unit,price,orderQty,totalQty) values('" + data.CUserid
                        + "','" + data.SUserid + "','" + dsGet.Tables[0].Rows[0]["srId"] + "','" + dsGet.Tables[0].Rows[0]["serviceName"]
                        + "','" + dsGet.Tables[0].Rows[0]["proId"] + "','" + dsGet.Tables[0].Rows[0]["productName"]
                        + "','" + dsGet.Tables[0].Rows[0]["unit"] + "','" + priceRate + "','1','1')";
                    }
                }
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


        /* DELETE api/<controller>/1 */
        public string DataDL(int id,string val)
        {
            string query = "delete from tbl.Cart where cartId='" + id + "'";
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
    }

    
}