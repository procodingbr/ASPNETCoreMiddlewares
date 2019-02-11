using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProCoding.Demos.ASPNetCore.Middlewares
{
    public class MyConventionBasedMiddleware
    {
        private readonly RequestDelegate _next;

        public MyConventionBasedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync(" Inicio do meu Convention Based Middleware! ");

            await _next.Invoke(context);

            await context.Response.WriteAsync(" Final do meu Convention Based Middleware! ");
        }
    }
}