using Microsoft.EntityFrameworkCore;

namespace BaseTemplate.Infrastructure.Persistence;

/// <summary>
/// Application database context for Entity Framework Core.
/// Add DbSet properties here for your domain entities.
/// Example: public DbSet<ProductEntity> Products { get; set; }
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>Initializes a new instance of the ApplicationDbContext class.</summary>
    /// <param name="options">DbContext options for configuration</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // TODO: Add DbSet properties for your domain entities here
    // Example:
    // public DbSet<YourEntity> YourEntities { get; set; }

    /// <summary>Configures the model when the context is being created.</summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TODO: Add entity configurations here
        // Example:
        // modelBuilder.ApplyConfiguration(new YourEntityConfiguration());
    }
}
