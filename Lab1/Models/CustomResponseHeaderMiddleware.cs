using Microsoft.Extensions.Options;

namespace Lab1.Models
{
    public class CustomResponseHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomResponseHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //To add Headers AFTER everything you need to do this
            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000"); //Strict-Transport-Security
                httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");  //X-Content-Type-Options
                httpContext.Response.Headers.Add("X-Xss-Protection", "1; mode=block"); //X-Xss-Protection
                //httpContext.Response.Headers.Add("Content-Security-Policy", "default-src 'self'"); //Content-Security-Policy - getting error when added this
                //Permissions-Policy
                httpContext.Response.Headers.Add("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");
                httpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN"); //HeadersX - Frame - Options

                /*
                                httpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                                httpContext.Response.Headers.Add("X-Xss-Protection", "1");
                                httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                                httpContext.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");*/
                //... and so on
                return Task.CompletedTask;
            }, context);

            await _next(context);
        }
    }
}
