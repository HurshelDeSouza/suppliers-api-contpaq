using Common.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Suppliers.API.Attributes;

public class IpRestrictionFilter(List<IpConfig> allowedIps) : IAsyncActionFilter
{
    private readonly List<IpConfig> _allowedIps = allowedIps ?? [];

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var remoteIp = GetClientIP(context.HttpContext);

        var allowedIp = _allowedIps.FirstOrDefault(ip => ip.IPv4 == remoteIp);
        if (string.IsNullOrEmpty(remoteIp) || allowedIp == null)
        {
            Loggers.MensajeError($"Invalid IP: {remoteIp}\nAuthorizeds: {string.Join(", ", allowedIps.Select(x => x.IPv4))}");
            context.Result = new StatusCodeResult(403); // Forbidden
            return;
        }

        Loggers.MensajeInformation($"Authorized IP: {allowedIp.Name} - {remoteIp}");
        await next();
    }

    private static string GetClientIP(HttpContext context)
    {
        // Intentar obtener de cabeceras primero (si hay proxy)
        if (context.Request.Headers.TryGetValue("X-Real-IP", out var realIp) && !StringValues.IsNullOrEmpty(realIp))
        {
            Loggers.MensajeInformation($"X-Real-IP found: {realIp}");
            return realIp.ToString().Split(',')[0].Trim();
        }

        if (context.Request.Headers.TryGetValue("X-Forwarded-For", out StringValues value) && !StringValues.IsNullOrEmpty(value))
        {
            Loggers.MensajeInformation($"X-Forwarded-For found: {value}");
            return value.ToString().Split(',')[0].Trim();
        }

        // Fallback a la conexión directa
        return context.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    }
}