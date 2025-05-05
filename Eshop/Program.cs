using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DbContext; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EShop", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EShop v1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapGet("/", () => "Hello World!");

app.Run();
