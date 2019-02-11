using Microsoft.AspNetCore.Builder;

namespace ProCoding.Demos.ASPNetCore.Middlewares
{
    public static class MyConventionBasedMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyConventionBasedMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<MyConventionBasedMiddleware>();
        
    }
}