namespace ProductAPI.Extensions;

public static class DefatulConfigurations
{
    public static WebApplicationBuilder ConfigureDefault(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Listen(IPAddress.Any, 80);
        });
        
    return builder;
    }
}
