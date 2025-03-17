namespace Application.Kafka.Producers;

public interface IKafkaProducerService
{
    Task ProduceToCreateAsync(CreateProductMessage productEvent, CancellationToken cts);
    Task ProduceToDeleteAsync(int id, CancellationToken cts);


}
