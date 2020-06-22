using Microsoft.AspNetCore.Mvc;
using Tutorial_12_Order.Models;
using Tutorial_12_Order.Services;

namespace Tutorial_12_Order.Controllers
{
    public class OrderController : ControllerBase
    {

        private IOrderDbService _service;

        public OrderController(IOrderDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetOrders(string name)
        {
            var res = _service.getOrders(name);
            return Ok(res);
        }
        
    }
}