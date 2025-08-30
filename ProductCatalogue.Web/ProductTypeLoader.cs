namespace ProductCatalogue.Web
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public class ProductTypeLoader
    {
        private readonly AppDbContext _dbContext;

        public ProductTypeLoader(AppDbContext dbContext)
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
            var productTypes = csv.GetRecords<ProductType>().ToList();

            // Optional: Clear existing data
            if (_dbContext.ProductType.Any())
            {
                _dbContext.ProductType.RemoveRange(_dbContext.ProductType);
                _dbContext.SaveChanges();
            }

            _dbContext.ProductType.AddRange(productTypes);
            _dbContext.SaveChanges();
        }
    }
}
