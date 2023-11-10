using Microsoft.EntityFrameworkCore;
using TestApp.Data.Models;

namespace TestApp.Data;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TagToUser> TagsToUsers { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<TagToUser>()
            .HasKey(t => new { t.UserId, t.TagId });

        modelBuilder.Entity<Tag>()
            .HasKey(t => t.TagId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Tags)
            .WithOne(tu => tu.User)
            .HasForeignKey(tu => tu.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}