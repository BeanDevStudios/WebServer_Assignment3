using System.Runtime.CompilerServices;

namespace WebServerApp_Week3.Middleware
{
    public static class MiddlewareExtensionMethods
    {
        public static IApplicationBuilder UseAlerts(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AlertMiddleware>();
        }

        //public static IApplicationBuilder UseQueryStrings(this IApplicationBuilder app)
        //{
        //    return app.UseMiddleware<QueryStringMiddleware>();
        //}
    }
}
