using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, Greeter>();
            services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            IGreeter greeter,
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            //app.Use(next =>
            //{
            //    return async context =>
            //    {
            //        logger.LogInformation(string.Format("Request incoming {0}, {1}", context.Request.Path, context.Request.QueryString));

            //        if (context.Request.Path.StartsWithSegments("/mym"))
            //        {
            //            await context.Response.WriteAsync("Hit!!!");
            //            logger.LogInformation("Request handled");
            //        }
            //        else
            //        {
            //            logger.LogInformation("Request dispatchd to next middleware component");
            //            await next(context);
            //        }
            //    };
            //});

            // Order matters!!
            app.UseStaticFiles();
            //app.UseDefaultFiles();
            // UseFileServer enables UseStaticFiles() and UseDefaultFiles()
            //app.UseFileServer(new FileServerOptions { EnableDirectoryBrowsing = false });

            app.UseMvc(ConfigureRoutes);

            //app.UseWelcomePage(new WelcomePageOptions { Path = "/welcome" });

            app.Run(async (context) =>
            {
                var greeting = greeter.GetMessageOfTheDay();
                logger.LogInformation($"{greeting} : {env.EnvironmentName}");
                await context.Response.WriteAsync(greeting);
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
