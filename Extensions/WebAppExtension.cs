using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;

namespace TaskManagement.Extensions
{
    public static class WebAppExtension
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
