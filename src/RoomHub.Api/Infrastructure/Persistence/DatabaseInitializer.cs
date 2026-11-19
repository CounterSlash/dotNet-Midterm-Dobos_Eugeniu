using Microsoft.EntityFrameworkCore;

namespace RoomHub.Api.Infrastructure.Persistence;

public static class DatabaseInitializer
{
    public static async Task InitializeDatabaseAsync(this IApplicationBuilder app)
    {
        await using var scope = app.ApplicationServices.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<RoomHubDbContext>();
        await db.Database.EnsureCreatedAsync();
    }
}
