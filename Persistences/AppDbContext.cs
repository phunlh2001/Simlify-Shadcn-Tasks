using Microsoft.EntityFrameworkCore;
using TaskManagement.Persistences.Entities;
using TaskManagement.Persistences.Extensions;

namespace TaskManagement.Persistences
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.SetUp();
        }

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaskTag> TaskTags { get; set; }
    }
}
