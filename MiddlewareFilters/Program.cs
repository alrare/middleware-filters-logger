using HandlebarsDotNet;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Extensions.Logging;
using MiddlewareFilters.Filters;
using MiddlewareFilters.Middleware;
using WireMock.Util;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);



//// Add services to the container.
//builder.Services.AddScoped<FiltersExample>();
builder.Services.AddScoped<FiltersExceptionHandler>();




builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//// Agregar el Middleware
//app.UseMiddleware<MiddlewareExample>();
app.UseMiddleware<MiddlewareException>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();