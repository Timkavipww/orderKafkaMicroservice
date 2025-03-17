namespace Application.Interfaces;

public interface IOrderService
{
    Task AddOrder(CreateOrderMessage order, CancellationToken cts);
    Task<List<Product>> GetProductsAsync(CancellationToken cts);
    Task<List<OrderSummary>> GetOrdersSummary();
}
