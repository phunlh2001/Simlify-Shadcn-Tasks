using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Models;
using TaskManagement.Extensions;

namespace TaskManagement.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.SetUp();
        }

        internal async Task FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaskTag> TaskTags { get; set; }
    }
}
