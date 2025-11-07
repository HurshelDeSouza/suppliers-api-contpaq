using Common.API.Program;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using Suppliers.API.Attributes;
using Suppliers.BL;
using Suppliers.BL.Bl;
using Suppliers.BL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Obtiene la colección de servicios para registrar dependencias
var services = builder.Services;

// Configuración de la base
services.AddDbContext<DescargaCfdiGfpContext>((serviceProvider, dbContext) =>
{
    var connectionString = builder.Configuration.GetConnectionString("SQLConnectionMain");
    dbContext.UseSqlServer(connectionString, op => op.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds));
});

services.AddScoped<IAuthBl, AuthBl>();
services.AddScoped<IReportBl, ReportBl>();

var allowedIps = builder.Configuration.GetSection("AllowedIpsConfig").Get<List<IpConfig>>();
services.AddSingleton(allowedIps);
//builder.Services.Configure<List<IpConfig>>(configuration.GetSection("AllowedIps"));
builder.Services.AddScoped<IpRestrictionFilter>();

QuestPDF.Settings.License = LicenseType.Community;

// Llama al método Main de GenericProgram para configurar y arrancar la aplicación
GenericProgram<uint>.Main(builder, services, 5010, "Suppliers API");