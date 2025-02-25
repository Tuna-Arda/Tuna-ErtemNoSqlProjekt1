
using System; // Oft nützlich für grundlegende Klassen (optional)
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Beispiel: Eigene Namespaces/Services
using JetstreamService.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Lade Konfiguration aus appsettings.json
// (Stelle sicher, dass diese Datei im Projekt enthalten ist und "Copy to Output Directory" angepasst ist)
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Dependency Injection (Services registrieren)
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IUserService, UserService>();

// MVC/Controller-Unterstützung
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger nur in Development aktivieren (optional)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Routing aktivieren
app.UseRouting();

// (Optional) Autorisierung
app.UseAuthorization();

// Controller-Routing
app.MapControllers();

// Anwendung starten
app.Run();
 