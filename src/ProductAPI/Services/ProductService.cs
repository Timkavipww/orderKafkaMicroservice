namespace ProductAPI.Services;


public class ProductService
(
    IProducer<Null, string> _producer,
    ProductDbContext _context
) : IProductService
{

    public async Task Add(CreateProductDTO product, CancellationToken cts)
    {
        var DTO = new Product
        {
            Name = product.Name,
            Price = product.Price
        };

        var result = await _producer.ProduceAsync(KafkaTopics.ADDPRODUCTTOPIC, 
            new Message<Null, string>{ Value = JsonSerializer.Serialize(product) }, cts);
        
        if (result.Status != PersistenceStatus.Persisted)
        {
            var lastProduct = _context.Products.Last();
            _context.Products.Remove(lastProduct);
            await _context.SaveChangesAsync();
        }
        


    }

    public async Task Delete(int id, CancellationToken cts)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id == id);
        if(product is null)
            throw new Exception("not found");

        _context.Products.Remove(product!);
        await _context.SaveChangesAsync();
        await _producer.ProduceAsync(KafkaTopics.DELETEPRODUCTTOPIC, 
        new Message<Null, string> {
            Value = id.ToString()
        },cts);
    }

}