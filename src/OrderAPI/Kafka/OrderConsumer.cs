
namespace OrderAPI.Kafka;

public class OrderConsumer
(
    ProductDbContext _productContext,
    IConsumer<Null, string> _consumer,
    ILogger<OrderConsumer> _logger
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cts)
    {
        _logger.LogInformation("Kafka consumer started...");
        await ConsumeAsync(cts);
    }
    public async Task ConsumeAsync(CancellationToken cts)
    {
        await Task.Delay(10);

        _consumer.Subscribe(KafkaTopics.ADDPRODUCTTOPIC);
        try
        {
            while (!cts.IsCancellationRequested)
            {
                var response = _consumer.Consume(cts);
                if(response.Message?.Value is not null)
                {
                    await ProcessMessageAsync(response, cts);
                    _consumer.Commit(response);
                }
            }
            _consumer.Unsubscribe();
        }
        
        catch (OperationCanceledException)
        {
            _consumer.Close();
        }
        
    }

    private async Task ProcessMessageAsync(ConsumeResult<Null, string> response, CancellationToken cts)
    {
        if (response.Topic == KafkaTopics.ADDPRODUCTTOPIC)
        {
            var product = JsonSerializer.Deserialize<Product>(response.Message.Value)!;
            await _productContext.Products.AddAsync(product, cts);
            await _productContext.SaveChangesAsync();
        }
        else if (response.Topic == KafkaTopics.DELETEPRODUCTTOPIC)
        {
            var productId = int.Parse(response.Message.Value);
            var product = await _productContext.Products.FirstOrDefaultAsync(x => x.Id == productId, cts);
            if (product != null)
            {
                _productContext.Products.Remove(product);
                await _productContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Product with ID {productId} not found for deletion.");
            }
        }
    }
    public override void Dispose()
    {
        _consumer.Unsubscribe();
        _consumer.Close();
        _consumer.Dispose();
        base.Dispose();
    }

}
