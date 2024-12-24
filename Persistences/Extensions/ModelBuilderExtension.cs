using Microsoft.EntityFrameworkCore;
using TaskManagement.Persistences.Entities;

namespace TaskManagement.Persistences.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void SetUp(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>(b =>
            {
                b.Property(x => x.Title).IsRequired();
                b.Property(x => x.Name).IsRequired();
                b.Property(x => x.Status).HasMaxLength(30);
                b.Property(x => x.Priority).HasMaxLength(30);

                b.HasMany(x => x.Tags)
                    .WithMany(b => b.Tasks)
                    .UsingEntity<TaskTag>();
            });

            modelBuilder.Entity<Tag>(b =>
            {
                b.Property(x => x.Name).HasMaxLength(15).IsRequired();
            });
        }
    }
}
