using Domain;

namespace OrderAPI.Services;

public interface IOrderService
{
    Task AddOrder(CreateOrder order, CancellationToken cts);
    Task<List<Product>> GetProductsAsync(CancellationToken cts);
    Task<List<OrderSummary>> GetOrdersSummary();
}
