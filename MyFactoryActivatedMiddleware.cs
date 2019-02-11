using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ProCoding.Demos.ASPNetCore.Middlewares
{
    public class MyFactoryActivatedMiddleware : IMiddleware
    {
        private readonly IConfiguration _config;

        public MyFactoryActivatedMiddleware(IConfiguration config)
        {
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync(" Inicio do meu Factory Activated Middleware! ");

            await next.Invoke(context);

            await context.Response.WriteAsync(" Final do meu Factory Activated Middleware! ");
        }
    }
}