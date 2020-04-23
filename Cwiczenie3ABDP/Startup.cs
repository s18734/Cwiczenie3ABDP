using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Cwiczenie3ABDP.DAL;
using Cwiczenie3ABDP.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Cwiczenie3ABDP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidIssuer = "Gakko",
                            ValidAudience = "student",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
                        };
                    });
           //dodac polaczenie z dbservice ktoryu stworzymy z metoda check addTransient<IDBService, DbService>
            services.AddSingleton<IDbService, DBServiceMS>();
            services.AddControllers();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbService dbService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //zad2
            app.UseMiddleware<LoggingMiddleware>();
            //middlewary wy¿ej maja pierwszeñstwo wiec zadanie 2 trzeba zrobiæ nad tym middlewarem
            //zad1
            //app.Use(async (context, next) =>
            //{

            //    //tutaj obslugujemy zadanie tylko od studenta
            //    if (!context.Request.Headers.ContainsKey("Index"))
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        await context.Response.WriteAsync("Nie podano indeksu w nag³ówku");
            //        return;
            //    }
                
              

            //    string index = context.Request.Headers["Index"].ToString();

            //    //³¹czenie z baza danych
            //    if (!dbService.CheckIndex(index))
            //    {
            //        //zrobienie logiki czy student jest w bazie danych
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        await context.Response.WriteAsync("Student nie istnieje w bazie");
            //        return;
            //        //coœ na podobie tego co jest wy¿ej
            //    }

            //    await next();
            //});


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
