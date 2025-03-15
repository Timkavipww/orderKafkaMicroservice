using System.Threading.Tasks;

namespace OrderAPI.Services;

public class OrderService
(
    OrderDbContext _orderContext,
    ProductDbContext _productContext
) : IOrderService
{


    public async Task AddOrder(CreateOrder order, CancellationToken cts)
    {
        await _orderContext.Orders.AddAsync(new Order
        {
            ProductId = order.ProductId,
            Quantity = order.Quantity
        },cts);
        await _orderContext.SaveChangesAsync();
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
