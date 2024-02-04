namespace WebServerApp_Week3.Middleware
{
    public class QueryStringMiddleware
    {

        RequestDelegate next;
        public QueryStringMiddleware(RequestDelegate Next)
        {
            next = Next;
        }
        public async Task InvokeAsync(HttpContext context) 
        {
            var queries = context.Request.Query;

            await next(context);
        }
    }
}
