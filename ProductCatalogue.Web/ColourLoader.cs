namespace ProductCatalogue.Web
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public class ColourLoader
    {
        private readonly AppDbContext _dbContext;

        public ColourLoader(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LoadColoursFromCsv(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";"
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);
            var colours = csv.GetRecords<Colour>().ToList();

            // Optional: Clear existing data
            if (_dbContext.Colour.Any())
            {
                _dbContext.Colour.RemoveRange(_dbContext.Colour);
                _dbContext.SaveChanges();
            }

            _dbContext.Colour.AddRange(colours);
            _dbContext.SaveChanges();
        }
    }
}
