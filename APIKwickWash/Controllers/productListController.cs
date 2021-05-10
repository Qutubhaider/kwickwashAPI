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
    public class productListController : ApiController
    {
        // GET: productList service wise
        public IEnumerable<product> Get(int id)
        {
            string query = "select * from tbl.myservice where userid='" + id + "' and status='1'";
            DataTable dt = Database.get_DataTable(query);
            List<product> product = new List<Models.product>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string queryProduct = "select proId,srId,serviceName,productName,unit,price,proImg,status,productCode,dropOffPrice,pickupDropPrice,productQty,minOrder from tbl.product where srId='" + dr["srId"].ToString() + "'";
                    DataSet dsPro = Database.get_DataSet(queryProduct);
                    foreach (DataRow drPro in dsPro.Tables[0].Rows)
                    {
                        string queryMyProduct = "select * from tbl.myproduct where proId='" + drPro["proId"].ToString() + "' and userid='" + id + "'";
                        DataSet ds_Mypro = Database.get_DataSet(queryMyProduct);
                        if (ds_Mypro.Tables[0].Rows.Count == 0)
                        {
                            product.Add(new ReadProduct(drPro));
                        }
                    }
                }
            }
            return product;
        }


        public List<product> GetWebPro(int id,string val)
        {
            string query = "select proId,srId,serviceName,productName,unit,(myprice) as price,proImg,status,productCode,(myprice1)as dropOffPrice,pickupDropPrice,productQty,minOrder from tbl.myproduct where userid='" + id + "' and status='1' order by srId asc";
            DataTable dt = Database.get_DataTable(query);
            List<product> product = new List<Models.product>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow drPro in dt.Rows)
                {
                    product.Add(new ReadProduct(drPro));
                }
            }
            return product;
        }


        public IEnumerable<product> GetProduct(int sid, string uid)
        {
            string queryProduct = "";
            List<product> product = new List<Models.product>();
            if (uid == "0")
            {
                queryProduct = "select proId,srId,serviceName,productName,unit,price,proImg,status,productCode,dropOffPrice,pickupDropPrice,productQty," +
                   "minOrder from tbl.product where srId='" + sid.ToString() + "'";                
            }
            else
            {
                queryProduct = "select proId,srId,serviceName,productName,unit,price,proImg,status,productCode,dropOffPrice,pickupDropPrice,productQty," +
                     "minOrder from tbl.myproduct where srId='" + sid.ToString() + "' and userid='" + uid.ToString() + "'";
            }
            DataSet dsPro = Database.get_DataSet(queryProduct);
            foreach (DataRow drPro in dsPro.Tables[0].Rows)
            {
                product.Add(new ReadProduct(drPro));
            }

            return product;
        }
    }
}