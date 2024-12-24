using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Persistences.Extensions
{
    public static class MigrationExtension
    {
        public static async Task Init(this WebApplication app)
        {
            var factory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = factory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}
