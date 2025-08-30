using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Product { get; set; }

    public DbSet<Product> ProductType { get; set; }

    public DbSet<Product> Colour { get; set; }
}