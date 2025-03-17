namespace ProductAPI.Services;

public class ProductService: IProductService
{
    
    // private readonly IProducer<string, DeleteProductMessage> _deleteProductProducer;
    private readonly IProducer<string, CreateProductMessage> _createProductProducer;

    public ProductService
    (
        // IProducer<string, DeleteProductMessage> deleteProductProducer,
        IProducer<string, CreateProductMessage> createProductProducer

    )
    {
        _createProductProducer = createProductProducer;
        // _deleteProductProducer = deleteProductProducer;
    }
    public async Task Add(CreateProductMessage product, CancellationToken cts)
    {
        await _createProductProducer.ProduceAsync(KafkaTopics.ADDPRODUCTTOPIC, new Message<string, CreateProductMessage>
        {
            Value = product,
            Key = Guid.CreateVersion7().ToString()
        });
    }

    public Task Delete(DeleteProductMessage message, CancellationToken cts)
    {
        // await _deleteProductProducer.ProduceAsync(KafkaTopics.DELETEPRODUCTTOPIC, new Message<string, DeleteProductMessage>
        // {
        //     Value = message,
        //     Key = Guid.CreateVersion7().ToString()
        // });
        return Task.CompletedTask;
    }

}