using SuperMarket.DomainModel;
using SuperMarket.Interfaces;

namespace SuperMarket.Orders
{
    public class OrderNotifier : IOrderObserver
    {
        public OrderNotifier()
        {
        }

        public Task OnOrderStatusChangedAsync(Order order, OrderStatus oldStatus)
        {
            throw new NotImplementedException();
        }
    }
}
