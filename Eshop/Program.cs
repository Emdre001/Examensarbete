using Microsoft.EntityFrameworkCore;
using DbContext;
using DbRepos;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.MaxDepth = 100; // Optional: increase if needed
});

//CORS stuff goes here

// Fetch the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("AzureSqlEShop");

// Add DbContext to DI container (use Npgsql for PostgreSQL)
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services for Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repos goes here
builder.Services.AddScoped<AdminDbRepos>();
builder.Services.AddScoped<BrandDbRepos>();
builder.Services.AddScoped<ColorDbRepos>(); 
builder.Services.AddScoped<OrderDbRepos>();
builder.Services.AddScoped<ProductDbRepos>();
builder.Services.AddScoped<SizeDbRepos>(); 


var app = builder.Build();

// Use Swagger in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
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