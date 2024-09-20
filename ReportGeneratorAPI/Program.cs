using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReportGeneratorAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Adicionar o serviço RabbitMQ
builder.Services.AddSingleton<RabbitMQService>();

// Adicionar os controllers
builder.Services.AddControllers();

var app = builder.Build();

// Use CORS
app.UseCors("AllowAngularOrigin");

app.MapControllers();
app.Run();
