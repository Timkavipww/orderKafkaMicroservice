namespace KafkaConsumer.Kafka.Handlers;

public class OrderCreatedMessageHandler : IMessageHandler<CreateOrderMessage>
{
    private readonly ILogger<OrderCreatedMessageHandler> _logger;
    private readonly OrderDbContext _orderDbContext;
    public OrderCreatedMessageHandler
    (
        ILogger<OrderCreatedMessageHandler> logger,
        OrderDbContext orderDbContext
    )
    {
        _orderDbContext = orderDbContext;
        _logger = logger;
    }
    

    
    public async Task HandleAsync(CreateOrderMessage message, CancellationToken cts)
    {
        if(message is null)
            return;

        var order = new Order
        {
            ProductId  = message.ProductId,
            Quantity = message.Quantity
        };

        await _orderDbContext.Orders.AddAsync(order);
        await _orderDbContext.SaveChangesAsync();
        _logger.LogInformation($"Заказ создан. {message.ProductId} | {message.Quantity}");

        
    }

}
// public async Task Consume(ConsumeContext<CreateOrder> context)
//     {
//         if (context.Message is not null)
//         {
//             var message = context.Message;
//             _logger.LogInformation($"get message {message.ToString()}");
//             var order = new Order
//             {
//                 ProductId = message.ProductId,
//                 Quantity = message.Quantity
//             };

//             if (order is not null)
//             {
//                 await _orderContext.Orders.AddAsync(order, context.CancellationToken);
//                 await _productContext.SaveChangesAsync();
//             }
//         }
//     }
//     public async Task Consume(ConsumeContext<DeleteProductMessage> context)
//     {
//         if (context.Message is not null)
//         {
//             var message = context.Message;
//             _logger.LogInformation($"get message {message.ToString()}");
//             var product = new Product
//             {
//                 Id = message.Id
//             };

//             if (product is not null)
//             {
//                 _productContext.Products.Remove(product);
//                 await _productContext.SaveChangesAsync();
//             }
//         }
//     }
//     public async Task Consume(ConsumeContext<CreateProductDTO> context)
//     {
//         if (context.Message is not null)
//         {
//             var message = context.Message;
//             _logger.LogInformation($"get message {message.ToString()}");
//             var product = new Product
//             {
//                 Name = message.Name,
//                 Price = message.Price
//             };

//             if (product is not null)
//             {
//                 await _productContext.Products.AddAsync(product, context.CancellationToken);
//                 await _productContext.SaveChangesAsync();
//             }
//         }

        
//     }

//     public async Task Consume(ConsumeContext<string> context)
//     {
//         if (context.Message is not null)
//         {
//             var message = context.Message;
//             _logger.LogInformation($"get message {message}");

//         }
//     }