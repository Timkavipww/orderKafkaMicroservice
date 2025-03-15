namespace OrderAPI.Extensions;

public static class MigrationsExtensions
{
    public static async Task<IApplicationBuilder> ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using OrderDbContext dbContext = 
            scope.ServiceProvider.GetRequiredService<OrderDbContext>();

        await dbContext.Database.MigrateAsync();

        return app;
    }
}