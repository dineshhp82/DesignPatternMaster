using SuperMarket.DomainModel;

namespace SuperMarket.Interfaces
{
    public interface IOrderObserver
    {
        Task OnOrderStatusChangedAsync(Order order, OrderStatus oldStatus);
    }
}
