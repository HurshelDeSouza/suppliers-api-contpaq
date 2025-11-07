using Common.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Suppliers.API.Attributes;

public class IpRestrictionAttribute : ActionFilterAttribute
{
    private readonly string[] _allowedIps;

    public IpRestrictionAttribute(params string[] allowedIps)
    {
        _allowedIps = allowedIps;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var remoteIp = GetClientIP(context.HttpContext);

        if (remoteIp == null || !_allowedIps.Contains(remoteIp))
        {
            Loggers.MensajeError($"Invalid IP: {remoteIp?.ToString()}");
            context.Result = new StatusCodeResult(403); // Forbidden
        }
        Loggers.MensajeInformation($"Authorized IP: {remoteIp?.ToString()}");
        base.OnActionExecuting(context);
    }

    private static string GetClientIP(HttpContext context)
    {
        //Intentar obtener de cabeceras primero(si hay proxy)
        if (context.Request.Headers.TryGetValue("X-Real-IP", out var realIp) && !StringValues.IsNullOrEmpty(realIp))
        {
            return realIp.ToString().Split(',')[0].Trim();
        }

        if (context.Request.Headers.TryGetValue("X-Forwarded-For", out StringValues value))
        {
            return value.ToString().Split(',')[0].Trim();
        }

        //Fallback a la conexión directa
        return context.Connection.RemoteIpAddress?.ToString() ?? "IP no disponible";
    }
}


public class IpConfig
{
    public string Name { get; set; }
    public string IPv4 { get; set; }
}

