namespace KafkaConsumer;

public static class Extensions
{
    public static IHostApplicationBuilder AddKafkaSupport(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));
        builder.Services.AddHostedService<OrderConsumerService>();
        builder.Services.AddScoped<IHostedService, OrderConsumerService>();

        builder.Services.AddScoped<IMessageHandler<CreateProductMessage>, CreateProductMessageHandler>();
        // builder.Services.AddScoped<IMessageHandler<CreateOrderMessage>, OrderCreatedMessageHandler>();
        // builder.Services.AddScoped<IMessageHandler<DeleteProductMessage>, ProductDeleteMessageHandler>();
        // builder.Services.AddSingleton<IProducer<string, DeleteProductMessage>>(sp =>
        // {
        //     var config = new ProducerConfig { BootstrapServers = "kafka:9092" };
        //     return new ProducerBuilder<string, DeleteProductMessage>(config).Build();
        // });
        builder.Services.AddSingleton<IProducer<string, CreateProductMessage>>(provider =>
        {
            var config = new ProducerConfig { BootstrapServers = "kafka:9092" };
            return new ProducerBuilder<string, CreateProductMessage>(config).Build();
        });
        // builder.Services.AddSingleton<IProducer<string, DeleteProductMessage>>(provider =>
        // {
        //     var config = new ProducerConfig { BootstrapServers = "kafka:9092" };
        //     return new ProducerBuilder<string, DeleteProductMessage>(config).Build();
        // });
        // builder.Services.AddSingleton<IProducer<string, CreateOrderMessage>>(provider =>
        // {
        //     var config = new ProducerConfig { BootstrapServers = "kafka:9092" };
        //     return new ProducerBuilder<string, CreateOrderMessage>(config).Build();
        // });

        // builder.Services.AddSingleton<IConsumer<string, DeleteProductMessage>>(provider =>
        // {
        //     var config = new ConsumerConfig
        //     {
        //         BootstrapServers = "kafka:9092",
        //         GroupId = "order-consumer-group",
        //         AutoOffsetReset = AutoOffsetReset.Earliest
        //     };
        //     return new ConsumerBuilder<string, DeleteProductMessage>(config).Build();
        // });
        // builder.Services.AddSingleton<IConsumer<string, CreateProductMessage>>(provider =>
        // {
        //     var config = new ConsumerConfig
        //     {
        //         BootstrapServers = "kafka:9092",
        //         GroupId = "product-consumer-group",
        //         AutoOffsetReset = AutoOffsetReset.Earliest
        //     };
        //     return new ConsumerBuilder<string, CreateProductMessage>(config).Build();
        // });

        // builder.Services.AddSingleton<IConsumer<string, CreateOrderMessage>>(provider =>
        // {
        //     var config = new ConsumerConfig
        //     {
        //         BootstrapServers = "kafka:9092",
        //         GroupId = "order-consumer-group",
        //         AutoOffsetReset = AutoOffsetReset.Earliest
        //     };
        //     return new ConsumerBuilder<string, CreateOrderMessage>(config).Build();
        // });





        return builder;
    }
}
