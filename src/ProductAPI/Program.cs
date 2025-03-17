var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDefault();
builder.AddData();
builder.AddServices();
builder.AddKafkaSupport();
builder.AddProductService();
var app = builder.Build();

await app.ApplyMigrations();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();
app.MapControllers();

app.Run();
