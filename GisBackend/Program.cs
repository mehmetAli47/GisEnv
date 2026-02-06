using GisBackend.Core.Application.Common.Interfaces;
using GisBackend.Core.Infrastructure.Persistence;
using GisBackend.Core.Infrastructure.Persistence.Repository;
using GisBackend.Core.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using GisBackend.Core.Application.Common.Behaviors;
using GisBackend.Middlewares;
using GisBackend.Core.Application.Common;

var builder = WebApplication.CreateBuilder(args);

// Mapster Configuration
MapsterConfig.Configure();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR Configuration
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    // Bekçilerimizi (Behaviors) sırayla ekliyoruz
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
});

// Unit of Work Kaydı
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// FluentValidation Kaydı
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Add DbContext registration here (e.g., using Entity Framework Core)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o=>o.UseNetTopologySuite()
    );
});

// Dependency Injection
builder.Services.AddScoped<ILayerRepository, LayerRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Hata Yakalama Sistemini Devreye Alıyoruz
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
