

using System.Net;
using OrderAPI.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 80);  // Слушаем на всех интерфейсах на порту 80
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ProductDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ProductDefaultConnection"));
});
builder.Services.AddDbContext<OrderDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrderDefaultConnection"));
});


builder.Services.AddScoped<IOrderService, OrderService>();

var config = new ConsumerConfig
{
    BootstrapServers = "kafka:9092",
    GroupId = "add-product-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest,
    AllowAutoCreateTopics = true
};
builder.Services.AddSingleton<IConsumer<Null, string>>(x => 
    new ConsumerBuilder<Null,string>(config).Build());

builder.Services.AddHostedService<OrderConsumer>();

var app = builder.Build();
await app.ApplyMigrations();


app.UseSwagger();
app.UseSwaggerUI();
// app.UseHttpsRedirection();

// app.UseAuthorization();

app.UseRouting();
app.MapControllers();

app.Run();
