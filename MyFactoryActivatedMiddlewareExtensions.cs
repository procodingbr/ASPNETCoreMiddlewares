using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ProCoding.Demos.ASPNetCore.Middlewares
{
    public static class MyFactoryActivatedMiddlewareExtensions
    {
        public static IServiceCollection AddMyFactoryActivatedMiddleware(this IServiceCollection services)
            => services.AddTransient<MyFactoryActivatedMiddleware>();

        public static IApplicationBuilder UseMyFactoryActivatedMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<MyFactoryActivatedMiddleware>();
    }
}