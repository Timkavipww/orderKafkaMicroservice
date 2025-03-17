namespace OrderAPI.Extensions;

public static class AddServicesExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IOrderService, OrderService>();
        
        return builder;
    }
}
