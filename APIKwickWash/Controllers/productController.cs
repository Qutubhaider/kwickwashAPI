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
    public class productController : ApiController
    {
        // GET: product
        public IEnumerable<product> Get()
        {
            string query = "select * from tbl.product";

            DataTable dt = Database.get_DataTable(query);
            List<product> product = new List<Models.product>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    product.Add(new ReadProduct(dr));
                }
            }
            return product;
        }
        public IEnumerable<product> Get(int id)
        {
            string query = "select * from tbl.product where proId='" + id + "'";

            DataTable dt = Database.get_DataTable(query);
            List<product> product = new List<Models.product>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    product.Add(new ReadProduct(dr));
                }
            }
            return product;
        }

        public string Post([FromBody]CreateProduct values)
        {
            try
            {
                string query_Product = "";
                if (values.proId == "0")
                {
                    query_Product = "insert into tbl.product (srId,serviceName,productName,unit,price,proImg,productCode,dropOffPrice,pickupDropPrice,productQty,minOrder) values ('" + values.srId
                    + "','" + values.serviceName + "','" + values.productName + "','" + values.unit + "','" + values.price
                    + "','" + values.proImg + "','" + values.productCode + "','" + values.dropOffPrice + "','" + values.pickupDropPrice + "','" + values.productQty + "','" + values.minOrder + "')";
                }
                else
                {
                    query_Product = "update tbl.product set srId='" + values.srId + "',serviceName='" + values.serviceName + "',productName='" + values.productName
                        + "',unit='" + values.unit + "',price='" + values.price + "',proImg='" + values.proImg + "',productCode='" + values.productCode
                        + "',dropOffPrice='" + values.dropOffPrice + "',pickupDropPrice='" + values.pickupDropPrice + "',productQty='" + values.productQty
                        + "',minOrder='" + values.minOrder + "' where proId='" + values.proId + "'";
                }
                int res = Database.Execute(query_Product);
                if (res == 1)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception ex)
            {
                return "false";
            }

        }
    }
}