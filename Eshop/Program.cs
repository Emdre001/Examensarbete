using Microsoft.EntityFrameworkCore;
using DbContext;
using DbRepos;
using DbRepos;

var builder = WebApplication.CreateBuilder(args);

// Fetch the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("AzureSqlEShop");

// Add DbContext to DI container (use Npgsql for PostgreSQL)
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlEShop")));

// Add services for Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<BrandDbRepos>();
builder.Services.AddScoped<AdminDbRepos>();

var app = builder.Build();

// Use Swagger in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => "Hello World!");

// Example of a minimal endpoint using the MainDbContext
app.MapGet("/products", async (MainDbContext dbContext) =>
{
    var products = await dbContext.Products.ToListAsync();
    return Results.Ok(products);
});


app.UseAuthorization();
app.MapControllers();


app.UseAuthorization();
app.MapControllers();
app.Run();