using GisBackend.Application.Layers.Interface;
using GisBackend.Infrastructure.Persistence;
using GisBackend.Infrastructure.Persistence.Repository;
using GisBackend.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using GisBackend.Application.Layers.Queries;
using FluentValidation;
using GisBackend.Application.Common.Behaviors;
using GisBackend.Application.Layers.Commands;
using GisBackend.Middlewares;
using GisBackend.Application.Common;

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
    cfg.RegisterServicesFromAssembly(typeof(GetAllLayersQuery).Assembly);
    // Bekçilerimizi (Behaviors) sırayla ekliyoruz
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
});

// Unit of Work Kaydı
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// FluentValidation Kaydı
builder.Services.AddValidatorsFromAssembly(typeof(CreateLayerCommandValidator).Assembly);

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
