using System.Collections.Generic;

public class ProductService
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Sofa Plane Oats", Type = "Sofa", Colour = "White" },
        new Product { Id = 2, Name = "Sofa Plane Ocean", Type = "Sofa", Colour = "Blue" },
        new Product { Id = 3, Name = "Sofa Plane Lovely", Type = "Sofa", Colour = "Ruby" },
        new Product { Id = 4, Name = "Armchair Just Army", Type = "Armchair", Colour = "Mossgreen" },
        new Product { Id = 5, Name = "Armchair Just Cherry", Type = "Armchair", Colour = "Ruby" }
    };

    public IEnumerable<Product> GetProducts() => _products;
}