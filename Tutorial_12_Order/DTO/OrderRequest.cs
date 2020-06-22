using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tutorial_12_Order.Models;

namespace Tutorial_12_Order.DTO
{
    public class OrderRequest
    {
        public DateTime DateAccepted { get; set; }
        public string Notes { get; set; }
        public List<ConfectionaryRequest> Confectionery { get; set; }
    }
}