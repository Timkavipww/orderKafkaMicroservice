namespace KafkaConsumer.Kafka;

public class KafkaSettings
{
    public string BoostrapServers { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
}
