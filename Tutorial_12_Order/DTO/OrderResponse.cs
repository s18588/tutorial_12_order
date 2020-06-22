using System;
using System.Collections.Generic;
using Tutorial_12_Order.Models;

namespace Tutorial_12_Order.DTO
{
    public class OrderResponse
    {
        public int IdOrder { get; set; }
        public DateTime DateAccepted { get; set; }
        public DateTime DateFinishied { get; set; }
        public List<Confectionery> List { get; set; }
    }
}