namespace KafkaConsumer;

public static class Extensions
{
    public static IHostApplicationBuilder AddKafkaSupport(this IHostApplicationBuilder builder)
    {
        var kafkaConfig = new ProducerConfig();
        builder.Configuration.GetSection("Kafka").Bind(kafkaConfig);
        builder.Services.AddSingleton(kafkaConfig);

        return builder;
    }
}
