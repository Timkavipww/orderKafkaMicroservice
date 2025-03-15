using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 80);  // Слушаем на всех интерфейсах на порту 80
});

builder.Services.AddDbContext<ProductDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ProductDefaultConnection"));
});

builder.Services.AddScoped<IProductService, ProductService>();

var config = new ProducerConfig
{
    BootstrapServers = "kafka:9092"
};
builder.Services.AddSingleton<IProducer<Null, string>>
    (x => new ProducerBuilder<Null, string>(config).Build());

var app = builder.Build();

await app.ApplyMigrations();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.UseRouting();
app.MapControllers();


app.Run();
