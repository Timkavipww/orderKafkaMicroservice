using Domain;

namespace ProductAPI.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpPost]
    public async Task<IActionResult> AddProduct(CreateProductMessage product, CancellationToken cts)
    {
        await _productService.AddAsync(product, cts);
        return Created();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(int id, CancellationToken cts)
    {
        await _productService.DeleteAsync(id, cts);
        return NoContent();
    }
    
}
