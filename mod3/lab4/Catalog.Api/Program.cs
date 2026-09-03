using System.Text.Json;
using Catalog.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Host=localhost;Port=5432;Database=catalog;Username=postgres;Password=postgres";

var redisConfiguration = builder.Configuration["Redis:Configuration"] ?? "localhost:6379";

builder.Services.AddDbContext<CatalogDbContext>(o => o.UseNpgsql(connectionString));
builder.Services.AddStackExchangeRedisCache(o => o.Configuration = redisConfiguration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Health: verifica la conexión a la BD
app.MapGet("/health", async (CatalogDbContext db) =>
{
    var dbOk = await db.Database.CanConnectAsync();
    return dbOk
        ? Results.Ok(new { status = "healthy", database = "connected" })
        : Results.StatusCode(503);
});

// GET /products — cache-aside con Redis
app.MapGet("/products", async (CatalogDbContext db, IDistributedCache cache) =>
{
    var cached = await cache.GetStringAsync("catalog:products");
    if (cached is not null)
        return Results.Ok(JsonSerializer.Deserialize<List<Product>>(cached));

    var products = await db.Products.OrderBy(p => p.Id).ToListAsync();
    await cache.SetStringAsync(
        "catalog:products",
        JsonSerializer.Serialize(products),
        new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });

    return Results.Ok(products);
});

// POST /products — guarda en BD e invalida el cache
app.MapPost("/products", async (Product product, CatalogDbContext db, IDistributedCache cache) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    await cache.RemoveAsync("catalog:products");
    return Results.Created($"/products/{product.Id}", product);
});

// GET /info — hostname del contenedor (para demo de round-robin al escalar)
app.MapGet("/info", () => Results.Ok(new { host = Environment.MachineName, utc = DateTime.UtcNow }));

// En Development: crea el schema y siembra datos de prueba
if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    await db.Database.EnsureCreatedAsync();

    if (!await db.Products.AnyAsync())
    {
        db.Products.AddRange(
            new Product { Name = "Laptop", Price = 999.99m },
            new Product { Name = "Mouse", Price = 25.50m });
        await db.SaveChangesAsync();
    }
}

app.Run();

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
}
