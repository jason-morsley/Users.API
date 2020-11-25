using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Users.API.DbContexts;
using Users.API.Services;

namespace Users.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           services.AddControllers(setupAction =>
           {
               setupAction.ReturnHttpNotAcceptable = true;

           }).AddNewtonsoftJson(setupAction =>
                   {
                       setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                   })
               .AddXmlDataContractSerializerFormatters(); // Whichever Serializer is added first will be default.

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
             
           services.AddScoped<IUserRepository, UserRepository>();

           services.AddDbContext<UserContext>(options =>
           {
                options.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=UserDB;Trusted_Connection=True;");
           }); 
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder => 
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Something has gone wrong, Please try again later.");
                        // ToDo --> Logging
                    }));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
