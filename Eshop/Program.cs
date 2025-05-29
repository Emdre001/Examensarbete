using Microsoft.EntityFrameworkCore;
using DbContext;
using DbRepos;
using Microsoft.Extensions.FileProviders;
using System.IO;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.MaxDepth = 100;
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
builder.Services.AddScoped<UserDbRepos>();



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

app.UseCors("AllowLocalhost3000");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "img")),  // adjust path as needed
    RequestPath = "/img"
});

app.UseAuthorization();
app.MapControllers();
app.Run();