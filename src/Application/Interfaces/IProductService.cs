namespace Application.Interfaces;

public interface IProductService
{
    Task AddAsync(CreateProductMessage product, CancellationToken cts);
    Task DeleteAsync(int id, CancellationToken cts);
}
