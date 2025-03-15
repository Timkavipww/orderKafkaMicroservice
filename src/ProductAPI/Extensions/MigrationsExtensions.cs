namespace ProductAPI.Extensions;

public static class MigrationsExtensions
{
    public static async Task<IApplicationBuilder> ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ProductDbContext dbContext = 
            scope.ServiceProvider.GetRequiredService<ProductDbContext>();

        await dbContext.Database.MigrateAsync();

        return app;
    }
}