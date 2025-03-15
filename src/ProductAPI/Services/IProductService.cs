namespace ProductAPI.Services;

public interface IProductService
{
    Task Add(CreateProductDTO product, CancellationToken cts);
    Task Delete(int id, CancellationToken cts);
}