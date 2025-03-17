namespace KafkaConsumer.Kafka;

public static class KafkaConfigs
{
    public static ConsumerConfig CONSUMERCONFIG = new ConsumerConfig
    {
        BootstrapServers = "kafka:9092",
        GroupId = "consumer-group",
        AutoOffsetReset = AutoOffsetReset.Earliest,
        EnableAutoCommit = true,
        EnableAutoOffsetStore = true,
    };
    public static ProducerConfig PRODUCERCONFIG = new ProducerConfig
    {
        BootstrapServers = "kafka:9092",
        Acks = Acks.All,
        // EnableIdempotence = true,
        // MessageTimeoutMs = 20000,
        // RequestTimeoutMs = 20000,
        // MessageSendMaxRetries = 3,
        // RetryBackoffMs = 100,
        // LingerMs = 10,
        // BatchNumMessages = 100,
        // QueueBufferingMaxMessages = 1000,
        // QueueBufferingMaxKbytes = 1024,
        // AllowAutoCreateTopics = true
    };
}
