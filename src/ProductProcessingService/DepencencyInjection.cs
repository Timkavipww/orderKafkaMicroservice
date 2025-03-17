namespace ProductProcessingService;

public static class DepencencyInjection
{
    public static IHostApplicationBuilder AddProductService(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IKafkaProducerService, KafkaProducerService>();
        return builder;
    }
}
