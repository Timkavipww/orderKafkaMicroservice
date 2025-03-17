using KafkaConsumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 80);
});
builder.Services.AddDbContext<ProductDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ProductDefaultConnection"));
});
builder.Services.AddDbContext<OrderDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrderDefaultConnection"));
});

builder.Services.AddScoped<IProductService, ProductService>();

builder.AddKafkaSupport();


var app = builder.Build();

await app.ApplyMigrations();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.UseRouting();
app.MapControllers();

app.Run();
