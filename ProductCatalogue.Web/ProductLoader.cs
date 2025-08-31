namespace ProductCatalogue.Web
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;

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
                Delimiter = ";"
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);
            var products = csv.GetRecords<Product>().ToList();

            // Optional: Clear existing data
            if (_dbContext.Product.Any())
            {
                _dbContext.Product.RemoveRange(_dbContext.Product);
                _dbContext.SaveChanges();
            }

            _dbContext.Product.AddRange(products);
            _dbContext.SaveChanges();
        }
    }
}
