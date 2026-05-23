namespace APIGateway.Middlewares
{
    public class TokenCheckerMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            string requestPath = context.Request.Path.Value!;

            if (requestPath.Contains("account/login", StringComparison.InvariantCultureIgnoreCase)
                || requestPath.Contains("account/account", StringComparison.InvariantCultureIgnoreCase)
                || requestPath.Equals("/"))
            {
                await next(context);
            }
            else
            {
                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

                if (string.IsNullOrEmpty(authHeader))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Access Denied");
                }
                else
                {
                    await next(context);
                }
            }
        }
    }
}
