using HeperFiberli.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace HeperFiberli.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Project> Projects => Set<Project>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(builder =>
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Slug).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Description).HasMaxLength(2000);
            builder.HasIndex(c => c.Slug).IsUnique();
        });

        modelBuilder.Entity<Product>(builder =>
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Code).IsRequired().HasMaxLength(100);
            builder.Property(p => p.ShortDescription).HasMaxLength(500);
            builder.Property(p => p.Description).HasMaxLength(4000);
            builder.HasIndex(p => p.Code).IsUnique();

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Project>(builder =>
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.City).HasMaxLength(150);
            builder.Property(p => p.Country).HasMaxLength(150);
            builder.Property(p => p.CustomerName).HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(2000);
        });
    }
}
