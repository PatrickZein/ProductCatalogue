using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Web;
using ProductCatalogue.Web.Components;
using System;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext with SQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register loaders
builder.Services.AddTransient<ProductTypeLoader>();
builder.Services.AddTransient<ColourLoader>();
builder.Services.AddTransient<ProductLoader>();

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOutputCache();

var app = builder.Build();

// Create and seed the database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); // Creates the database if it doesn't exist

    var loader1 = scope.ServiceProvider.GetRequiredService<ProductTypeLoader>();
    loader1.LoadProductsFromCsv("product-types.csv");

    var loader2 = scope.ServiceProvider.GetRequiredService<ColourLoader>();
    loader2.LoadColoursFromCsv("colours.csv");

    var loader3 = scope.ServiceProvider.GetRequiredService<ProductLoader>();
    loader3.LoadProductsFromCsv("products.csv");
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
