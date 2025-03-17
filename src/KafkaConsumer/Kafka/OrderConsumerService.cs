namespace KafkaConsumer.Kafka;

public class OrderConsumerService : BackgroundService
{
    private readonly ILogger<OrderConsumerService> _logger;
    private readonly IConsumer<string, CreateProductMessage> _addProductConsumer;
    // private readonly IConsumer<string, DeleteProductMessage> _deleteProductConsumer;
    // private readonly IConsumer<string, CreateOrderMessage> _createOrderConsumer;
    // private readonly IMessageHandler<DeleteProductMessage> _deleteProductHandler;
    private readonly IMessageHandler<CreateProductMessage> _addProductHandler;
    // private readonly IMessageHandler<CreateOrderMessage> _createOrderHandler;

    public OrderConsumerService
    (
        ILogger<OrderConsumerService> logger,
        // IConsumer<string, DeleteProductMessage> deleteProductConsumer,
        IConsumer<string, CreateProductMessage> addProductConsumer,
        // IConsumer<string, CreateOrderMessage> createOrderConsumer,
        // IMessageHandler<DeleteProductMessage> deleteProductHandler,
        IMessageHandler<CreateProductMessage> addProductHandler
        // IMessageHandler<CreateOrderMessage> createOrderHandler

    )
    {
        // _createOrderConsumer = createOrderConsumer;
        // _createOrderHandler = createOrderHandler;
        // _deleteProductHandler = deleteProductHandler;
        _addProductHandler = addProductHandler;
        _addProductConsumer = addProductConsumer;
        // _deleteProductConsumer = deleteProductConsumer;
        _logger = logger;

        // _deleteProductConsumer = new ConsumerBuilder<string, DeleteProductMessage>(KafkaConfigs.CONSUMERCONFIG)
        //     .SetValueDeserializer(new KafkaDeserializer<DeleteProductMessage>())
        //     .Build();
        _addProductConsumer = new ConsumerBuilder<string, CreateProductMessage>(KafkaConfigs.CONSUMERCONFIG)
            .SetValueDeserializer(new KafkaDeserializer<CreateProductMessage>())
            .Build();
        // _createOrderConsumer = new ConsumerBuilder<string, CreateOrderMessage>(KafkaConfigs.CONSUMERCONFIG)
        //     .SetValueDeserializer(new KafkaDeserializer<CreateOrderMessage>())
        //     .Build();

        
    }

        

    protected async override Task ExecuteAsync(CancellationToken cts)
    {
        // _deleteProductConsumer.Subscribe(KafkaTopics.DELETEPRODUCTTOPIC);
        _addProductConsumer.Subscribe(KafkaTopics.ADDPRODUCTTOPIC);
        // _createOrderConsumer.Subscribe(KafkaTopics.CREATEORDERTOPIC);

        try
        {
            while(!cts.IsCancellationRequested)
            {
                await ConsumeAsync(cts)!;
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обработке Kafka сообщений");
        }
        finally
        {
            // _deleteProductConsumer.Close();
            _addProductConsumer.Close();
            // _createOrderConsumer.Close();
        }
        
}

private async Task? ConsumeAsync(CancellationToken cts)
    {
        try
        {
            // var deleteProductResult = _deleteProductConsumer.Consume(cts);
            var createProductResult = _addProductConsumer.Consume(cts);
            // var createOrderResult = _createOrderConsumer.Consume(cts);

            // if (deleteProductResult?.Message?.Value != null)
            // {
            //     await _deleteProductHandler.HandleAsync(deleteProductResult.Message.Value, cts);
            // }
            if (createProductResult?.Message?.Value != null)
            {
                await _addProductHandler.HandleAsync(createProductResult.Message.Value, cts);
            }
            // if (createOrderResult?.Message?.Value != null)
            // {
            //     await _createOrderHandler.HandleAsync(createOrderResult.Message.Value, cts);
            // }


        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Остановка Kafka ConsumerService");
        }
        catch (ConsumeException ex)
        {
            _logger.LogError(ex, $"Ошибка потребления Kafka: {ex.Error.Reason}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Неизвестная ошибка в Kafka Consumer");
        }
    }
}


