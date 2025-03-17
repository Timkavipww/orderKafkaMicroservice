namespace ProductProcessingService;

public class ProductService : IProductService
{
    private readonly IKafkaProducerService _kafkaProducer;
    public ProductService
    (
        IKafkaProducerService kafkaProducer
    )
    {
        _kafkaProducer = kafkaProducer;
    }
    public async Task AddAsync(CreateProductMessage product, CancellationToken cts)
    {
        await _kafkaProducer.ProduceToCreateAsync(product, cts);
    }

    public async Task DeleteAsync(int id, CancellationToken cts)
    {
        await _kafkaProducer.ProduceToDeleteAsync(id, cts);
    }

}