using System.Collections.Generic;

public class ProductService
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Sofa Plain Oats", Type = "Sofa", Colour = "White" },
        new Product { Id = 2, Name = "Sofa Plain Ocean", Type = "Sofa", Colour = "Blue" },
        new Product { Id = 3, Name = "Sofa Plain Lovely", Type = "Sofa", Colour = "Ruby" },
        new Product { Id = 4, Name = "Armchair Just Plain", Type = "Armchair", Colour = "White" },
        new Product { Id = 5, Name = "Armchair Just Dreamy", Type = "Armchair", Colour = "Blue" },
        new Product { Id = 6, Name = "Armchair Just Cherry", Type = "Armchair", Colour = "Ruby" }
        new Product { Id = 7, Name = "Armchair Just Army", Type = "Armchair", Colour = "Mossgreen" },
    };

    public IEnumerable<Product> GetProducts() => _products;
}