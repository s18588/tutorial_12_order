using System.Collections;
using Tutorial_12_Order.DTO;

namespace Tutorial_12_Order.Services
{
    public interface IOrderDbService
    {
        public IEnumerable getOrders(string name);
        public string PlaceOrder(int customerId, OrderRequest request);
    }    
}