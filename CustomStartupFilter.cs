using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ProCoding.Demos.ASPNetCore.Middlewares
{
    public class CustomStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return (IApplicationBuilder app) => {
                app.Use(async (c, n) => {
                    await c.Response.WriteAsync(" Inicio do CustomStartupFilter! ");
                    await n.Invoke();
                    await c.Response.WriteAsync(" Final do CustomStartupFilter! ");
                });

                next(app);
            };
        }
    }
}