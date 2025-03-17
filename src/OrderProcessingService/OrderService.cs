namespace OrderProcessingService;

public class OrderService
(
    OrderDbContext _orderContext,
    ProductDbContext _productContext
    // IProducer<string, CreateOrderMessage> _createOrderProducer
) : IOrderService
{
    public Task AddOrder(CreateOrderMessage order, CancellationToken cts)
    {
        var message = new Message<string, CreateOrderMessage>()
        {
            Key = Guid.CreateVersion7().ToString(),
            Value = order
        };
        // await _createOrderProducer.ProduceAsync(KafkaTopics.CREATEORDERTOPIC, message, cts);
        return Task.CompletedTask;
    }

    public async Task<List<OrderSummary>> GetOrdersSummary()
    {
        var products = await _productContext.Products.ToListAsync();
        var orders = await _orderContext.Orders.ToListAsync();
        var orderSummaries = new List<OrderSummary>();

        foreach (var order in orders)
        {
            var product = products.FirstOrDefault(x => x.Id == order.ProductId);

            orderSummaries.Add(new OrderSummary
            {
                OrderId = order.Id,
                ProductId = order.ProductId,
                ProductName = product != null ? product.Name : "Unknown",
                ProductPrice = product != null ? product.Price : 0,
                OrderedQuantity = order.Quantity
            });
        }

        await _orderContext.OrderSummaries.AddRangeAsync(orderSummaries);
        await _orderContext.SaveChangesAsync();

        return orderSummaries;
    }



    public async Task<List<Product>> GetProductsAsync(CancellationToken cts)
    {
        return await _productContext.Products.ToListAsync(cts);
    }


}
