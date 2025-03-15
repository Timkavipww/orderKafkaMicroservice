using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> AddProduct(CreateProductDTO product, CancellationToken cts)
    {
        await _productService.Add(product, cts);
        return Created();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(int id, CancellationToken cts)
    {
        await _productService.Delete(id, cts);
        return NoContent();
    }
}
