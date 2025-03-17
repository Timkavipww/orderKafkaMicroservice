namespace Application.Interfaces;

public interface IProductService
{
    Task Add(CreateProductMessage product, CancellationToken cts);
    Task Delete(DeleteProductMessage message, CancellationToken cts);
}
