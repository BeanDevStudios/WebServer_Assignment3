namespace WebServerApp_Week3.Middleware
{
    public class AlertMiddleware
    {
        RequestDelegate next;

        public AlertMiddleware(RequestDelegate Next)
        {
            next = Next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //DO Stuff here

            await next(context);
        }
    }
}
