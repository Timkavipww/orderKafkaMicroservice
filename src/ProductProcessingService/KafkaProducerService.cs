namespace ProductProcessingService;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducerService()
    {
        var config = new ProducerConfig { BootstrapServers = "kafka:9092" };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task ProduceToCreateAsync(CreateProductMessage productEvent, CancellationToken cts)
    {
        var message = JsonConvert.SerializeObject(productEvent);
        await _producer.ProduceAsync(KafkaTopics.CREATEORDERTOPIC, new Message<Null, string> { Value = message }, cts);
    }
    public async Task ProduceToDeleteAsync(int id, CancellationToken cts)
    {
        var entity = new DeleteProductMessage(){Id = id };
        var message = JsonConvert.SerializeObject(entity);

        await _producer.ProduceAsync(KafkaTopics.DELETEPRODUCTTOPIC, new Message<Null, string> { Value = message }, cts);
    }
}
