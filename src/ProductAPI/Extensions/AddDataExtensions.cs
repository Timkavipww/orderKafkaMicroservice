namespace ProductAPI.Extensions;

public static class AddDataExtensions
{
    public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
    {   
        builder.Services.AddDbContext<ProductDbContext>(options => 
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("ProductDefaultConnection"));
        });
        
        builder.Services.AddDbContext<OrderDbContext>(options => 
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("OrderDefaultConnection"));
        });

        return builder;
    }
}
