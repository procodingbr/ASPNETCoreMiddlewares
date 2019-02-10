using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ProCoding.Demos.ASPNetCore.Middlewares
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (context, next) => {
                await context.Response.WriteAsync(" Inicio do primeiro middleware!");
                await next.Invoke();
                await context.Response.WriteAsync(" Final do primeiro middleware!");
            });

            app.MapWhen(context => context.Request.Query.ContainsKey("condition"), ConditionalBranch);

            app.Map("/branch", ConfigureBranch);

            app.Use(async (context, next) => {
                await context.Response.WriteAsync(" Inicio do segundo middleware!");
                await next.Invoke();
                await context.Response.WriteAsync(" Final do segundo middleware!");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(" Final do Pipeline!");
            });
        }

        private void ConditionalBranch(IApplicationBuilder app)
        {

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($" Valor da condicao: {context.Request.Query["condition"]}");
            });
        }

        private void ConfigureBranch(IApplicationBuilder app)
        {
            app.Use(async (context, next) => {
                await context.Response.WriteAsync(" Início do primeiro Middleware condicional!");
                await next.Invoke();
                await context.Response.WriteAsync(" Final do primeiro Middleware condicional!");
            });

            app.Map("/first", ConfigureFirstBranch);  
            app.Map("/second", ConfigureSecondBranch);


            app.Use(async (context, next) => {
                await context.Response.WriteAsync(" Início do segundo Middleware condicional!");
                await next.Invoke();
                await context.Response.WriteAsync(" Final do segundo Middleware condicional!");
            });

            app.Run(async (context) => { 
                await context.Response.WriteAsync(" Final do pipeline condicional!");
            });
        }

        private void ConfigureSecondBranch(IApplicationBuilder app)
        {
            app.Run(async (context) => { 
                await context.Response.WriteAsync(" Primeiro sub-branch condicional!");
            });
        }

        private void ConfigureFirstBranch(IApplicationBuilder app)
        {
            app.Run(async (context) => { // Path: ""; BasePath: "/branch/first"
                await context.Response.WriteAsync(" Segundo sub-branch condicional!");
            });
        }
    }
}
