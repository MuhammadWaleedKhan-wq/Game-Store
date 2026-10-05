using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }

    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("GameStoreDb");
        
        // Dependency injection for GameStoreContext
        builder.Services.AddScoped<GameStoreContext>();

        // DbContext has a Scoped service lifetime because
        // 1. It ensures that a new instance of DbContext is created per request
        // 2. DB connections are a limited and expensive resources
        // 3. DbContext is not thread-safe, Scope avoids to concurrency issues
        // 4. Makes it easier to manage transactions and ensures data consistency
        // 5. Reusing a DbContext instance can lead to increased memory usage
        builder.Services.AddSqlite<GameStoreContext>(
            connString,
            // intended to seed the database with initial data if the Genre table is empty upon
            // application startup. This is a simple example of seeding data, and you can customize
            // the seeding logic based on your application's requirements.
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<Genre>().Any())
                {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Action" },
                        new Genre { Name = "Adventure" },
                        new Genre { Name = "RPG" },
                        new Genre { Name = "Simulation" },
                        new Genre { Name = "Strategy" }
                    );
                    context.SaveChanges();
                }
            }));
    }
}