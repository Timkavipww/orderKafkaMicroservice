namespace Application.Kafka.Messages;


public class CreateProductMessage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
