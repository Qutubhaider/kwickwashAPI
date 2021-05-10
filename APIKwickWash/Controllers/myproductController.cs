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
    public class myproductController : ApiController
    {
        // GET: myproduct
        public IEnumerable<myproduct> Get(int id)
        {
            string queryProduct = "select  prodId,proId,srId,serviceName,productName,unit,price,proImg,status,myPrice,productCode,dropOffPrice,pickupDropPrice,productQty,minOrder,myPrice1 from tbl.myproduct where userid='" + id + "' and status='1'";
            DataTable dt = Database.get_DataTable(queryProduct);
            List<myproduct> product = new List<Models.myproduct>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    product.Add(new ReadMyProduct(dr));

                }
            }
            return product;
        }

        public IEnumerable<myproduct> GetProductList(int id,string sid)
        {
            string queryProduct = "select top 16 prodId,proId,srId,serviceName,productName,unit,price,proImg,status,myPrice,productCode,dropOffPrice,pickupDropPrice,productQty,minOrder,myPrice1 from tbl.myproduct where userid='" + id + "' and status='1'";
            DataTable dt = Database.get_DataTable(queryProduct);
            List<myproduct> product = new List<Models.myproduct>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    product.Add(new ReadMyProduct(dr));

                }
            }
            return product;
        }

        public IEnumerable<myproduct> GetProduct(string sid, string uid, int pageno)
        {
            int curr = pageno;
            string pager = "";
            string queryProduct = "select prodId,proId,srId,serviceName,productName,unit,price,proImg,status,myPrice,productCode,dropOffPrice,pickupDropPrice," +
                "productQty,minOrder,myPrice1 from tbl.myproduct where userid='" + uid + "' and srid='" + sid + "' and status='1'";
            DataSet ds = Database.get_DataSet(queryProduct, "tbl.myproduct", curr, 16);
            DataTable dt = Database.get_DataTable(queryProduct);
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (dt.Rows.Count > 16)
                {
                    pager = ""; //Function.doPaging(curr, dt.Rows.Count, 6, ds.Tables[0].Rows[0]["categoryname"] + "-" + cateid.ToString() + "~");
                }
            }
            List<myproduct> myproduct = new List<myproduct>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    myproduct.Add(new ReadMyProduct(dr));
                }
            }
            return myproduct;
        }

        public IEnumerable<myproduct> GetProductApps(string sid, string uid)
        {  
            string queryProduct = "select prodId,proId,srId,serviceName,productName,unit,price,proImg,status,myPrice,productCode,dropOffPrice,pickupDropPrice," +
                "productQty,minOrder,myPrice1 from tbl.myproduct where userid='" + uid + "' and srid='" + sid + "' and status='1'";
            DataSet ds = Database.get_DataSet(queryProduct);
            List<myproduct> myproduct = new List<myproduct>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    myproduct.Add(new ReadMyProduct(dr));
                }
            }
            return myproduct;
        }

    }
}