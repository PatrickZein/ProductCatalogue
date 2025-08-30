builder.Services.AddTransient<ProductLoader>();

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var loader = scope.ServiceProvider.GetRequiredService<ProductTypesLoader>();
    loader.LoadProductTypesFromCsv("product-types.csv");

    loader = scope.ServiceProvider.GetRequiredService<ColoursLoader>();
    loader.LoadColoursFromCsv("colours.csv");

    loader = scope.ServiceProvider.GetRequiredService<ProductLoader>();
    loader.LoadProductsFromCsv("products.csv");
}