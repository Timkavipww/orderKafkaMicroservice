namespace Application.Kafka.Messages;

public class CreateOrderMessage
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
