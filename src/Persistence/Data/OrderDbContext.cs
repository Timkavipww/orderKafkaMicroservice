namespace Persistence.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
        
    }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderSummary> OrderSummaries { get; set;}
}
