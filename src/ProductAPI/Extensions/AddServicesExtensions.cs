namespace ProductAPI.Extensions;

public static class AddServicesExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProductService, ProductService>();
        return builder;
    }
}
