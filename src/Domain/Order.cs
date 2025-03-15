namespace Domain;

public class Order : BaseEntity
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrder
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
