using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ProCoding.Demos.ASPNetCore.Middlewares
{
    public static class MyCustomMiddlewareExtensions
    {
        private static void ConditionalBranch(IApplicationBuilder app)
        {

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($" Valor da condicao: {context.Request.Query["condition"]} ");
            });
        }

        private static void ConfigureBranch(IApplicationBuilder app)
        {
            app.Use(async (context, next) => {
                await context.Response.WriteAsync(" Início do primeiro Middleware condicional! ");
                await next.Invoke();
                await context.Response.WriteAsync(" Final do primeiro Middleware condicional! ");
            });

            app.Map("/first", ConfigureFirstBranch);  
            app.Map("/second", ConfigureSecondBranch);


            app.Use(async (context, next) => {
                await context.Response.WriteAsync(" Início do segundo Middleware condicional! ");
                await next.Invoke();
                await context.Response.WriteAsync(" Final do segundo Middleware condicional! ");
            });

            app.Run(async (context) => { 
                await context.Response.WriteAsync(" Final do pipeline condicional! ");
            });
        }

        private static void ConfigureSecondBranch(IApplicationBuilder app)
        {
            app.Run(async (context) => { 
                await context.Response.WriteAsync(" Primeiro sub-branch condicional! ");
            });
        }

        private static void ConfigureFirstBranch(IApplicationBuilder app)
        {
            app.Run(async (context) => { // Path: ""; BasePath: "/branch/first"
                await context.Response.WriteAsync(" Segundo sub-branch condicional! ");
            });
        }


        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
        {
            app.Use(async (context, next) => {
                await context.Response.WriteAsync(" Inicio do primeiro middleware! ");
                await next.Invoke();
                await context.Response.WriteAsync(" Final do primeiro middleware! ");
            });

            app.MapWhen(context => context.Request.Query.ContainsKey("condition"), ConditionalBranch);

            app.Map("/branch", ConfigureBranch);

            app.Use(async (context, next) => {
                await context.Response.WriteAsync(" Inicio do segundo middleware! ");
                await next.Invoke();
                await context.Response.WriteAsync(" Final do segundo middleware! ");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(" Final do Pipeline! ");
            });

            return app;
        }
    }
}