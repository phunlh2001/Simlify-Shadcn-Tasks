using Microsoft.EntityFrameworkCore;
using TaskManagement.Persistences.Entities;

namespace TaskManagement.Persistences
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TaskEntity>(b =>
            {
                b.Property(x => x.Title).HasMaxLength(1000).IsRequired();
                b.Property(x => x.Name).HasMaxLength(100).IsRequired();
                b.Property(x => x.Status).HasMaxLength(30).HasConversion<string>();
                b.Property(x => x.Priority).HasMaxLength(30).HasConversion<string>();

                b.HasMany(x => x.Tags)
                    .WithMany(b => b.Tasks)
                    .UsingEntity<TaskTag>();
            });

            builder.Entity<Tag>(b =>
            {
                b.Property(x => x.Name).HasMaxLength(15).IsRequired();
            });
        }

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaskTag> TaskTags { get; set; }
    }
}
