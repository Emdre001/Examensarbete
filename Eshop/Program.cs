using Microsoft.EntityFrameworkCore;
using DbContext;
using DbRepos;
using Microsoft.OpenApi.Models;
using Eshop.DbRepos;
using Microsoft.AspNetCore.Authentication;
using Eshop.Services;

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

// Database context registration
var connectionString = builder.Configuration.GetConnectionString("AzureSqlEShop");
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(connectionString));

// Repos registration
builder.Services.AddScoped<AdminDbRepos>();
builder.Services.AddScoped<BrandDbRepos>();
builder.Services.AddScoped<ColorDbRepos>(); 
builder.Services.AddScoped<OrderDbRepos>();
builder.Services.AddScoped<ProductDbRepos>();
builder.Services.AddScoped<SizeDbRepos>();
builder.Services.AddScoped<UserDbRepos>();
builder.Services.AddScoped<AccountRepos>();

builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformationService>();

// Swagger with JWT support
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformationService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "E Shop API",
        Version = "v1"
    });

    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        Description = "Basic Authentication header"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();