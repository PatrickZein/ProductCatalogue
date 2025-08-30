using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

public class ProductLoader
{
    private readonly AppDbContext _dbContext;

    public ProductLoader(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void LoadProductsFromCsv(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, config);
        var products = csv.GetRecords<Product>().ToList();

        _dbContext.Products.AddRange(products);
        _dbContext.SaveChanges();
    }
}