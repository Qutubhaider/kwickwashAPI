using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace APIKwickWash.Models
{
    public class DriverCounter
    {
        public string NewOrder { get; set; }
        public string ProcessOrder { get; set; }
        public string DeliveredOrder { get; set; }
        public string TotalOrder { get; set; }
    }

   
}