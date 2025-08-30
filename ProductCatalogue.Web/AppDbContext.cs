namespace ProductCatalogue.Web
{
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Colour> Colour { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
