using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIKwickWash.Models
{
    public class CustomerLedger
    {
        public string companyName { get; set; }
        public int orderNo { get; set; }
        public int invoiceNo { get; set; }
        public string orderType { get; set; }
        public string dropRequest { get; set; }
        public string entryDate { get; set; }
        public decimal? debiteAmount { get; set; }
        public decimal? creditAmount { get; set; }
        public decimal? balance { get; set; }
        public decimal? ttlorder { get; set; }
        public decimal? ttlorderamt { get; set; }
        public decimal? ttlpaidamt { get; set; }
        public decimal? ttlunpaidamt { get; set; }
    }
}