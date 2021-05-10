using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace APIKwickWash.Models
{
    public class addOn
    {
        public string AddOnId { get; set; }
        public string OrderId { get; set; }
        public string ProdctName { get; set; }
        public string Qty { get; set; }
        public string Price { get; set; }
        public string TotalPrice { get; set; }
    }

    public class CreateaddOn:addOn
    {

    }

    public class ReadaddOn:addOn
    {
        public ReadaddOn(DataRow dr)
        {
            AddOnId = dr["AddOnId"].ToString();
            OrderId = dr["OrderId"].ToString();
            ProdctName = dr["ProdctName"].ToString();
            Qty = dr["Qty"].ToString();
            Price = dr["Price"].ToString();
            TotalPrice = dr["TotalPrice"].ToString();
        }
    }
}