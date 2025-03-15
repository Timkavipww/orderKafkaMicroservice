using System.Threading.Tasks;

namespace OrderAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController
(
    IOrderService _orderService
) : ControllerBase
{

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts(CancellationToken cts)
    {
        var products = await _orderService.GetProductsAsync(cts);

        return Ok(products);
    }

    [HttpPost("add")]
    public IActionResult AddOrder(CreateOrder order, CancellationToken cts)
    {

        _orderService.AddOrder(order, cts);
        return Ok($"Order created");
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersSummary()
    {
        var ordSum = await _orderService.GetOrdersSummary();
        return Ok(ordSum);
    }
}
