using Microsoft.Extensions.Primitives;
using System.Configuration;

namespace WebServerApp_Week3.Middleware
{
    public class IpSecurityMiddleware
    {
        RequestDelegate next;

        public IpSecurityMiddleware(RequestDelegate Next)
        {
            next = Next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string ipAddress = context.Request.HttpContext.Connection.RemoteIpAddress.ToString();

            context.Request.HttpContext.Items["ip-address"] = ipAddress;

            await next(context);

            var keyValuePair = new KeyValuePair<string, StringValues>("ClientIP", new StringValues(ipAddress));

            context.Response.Headers.Add(keyValuePair);
        }
    }
}
