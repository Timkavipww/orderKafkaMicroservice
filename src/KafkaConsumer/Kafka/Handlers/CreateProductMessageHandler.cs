namespace KafkaConsumer.Kafka.Handlers;

public class CreateProductMessageHandler : IMessageHandler<CreateProductMessage>
{
    private readonly ILogger<OrderCreatedMessageHandler> _logger;
    private readonly ProductDbContext _productDbContext;
    public CreateProductMessageHandler
    (
        ILogger<OrderCreatedMessageHandler> logger,
        ProductDbContext productDbContext
    )
    {
        _productDbContext = productDbContext;
        _logger = logger;
    }

    public async Task HandleAsync(CreateProductMessage message, CancellationToken cts)
    {
         if(message is null)
            return;

        var product = new Product
        {
            Name = message.Name,
            Price = message.Price,
            Id = message.Id
        };

            await _productDbContext.Products.AddAsync(product);
            await _productDbContext.SaveChangesAsync();
        _logger.LogInformation($"Продукт добавлен. {message.Id} ");
    }
}