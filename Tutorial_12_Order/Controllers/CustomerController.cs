using Microsoft.AspNetCore.Mvc;
using Tutorial_12_Order.DTO;
using Tutorial_12_Order.Services;

namespace Tutorial_12_Order.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private OrderDbService _service;

        public CustomerController(OrderDbService service)
        {
            _service = service;
        }

        [HttpPost("{id}/orders")]
        public IActionResult PlaceOrder(int id, OrderRequest req)
        {
            var order = _service.PlaceOrder(id, req);
            return Ok(order);
        }
    }
}