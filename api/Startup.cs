// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Models;
// using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
// using Newtonsoft.Json.Serialization;

// namespace projnet
// {
//     public class Startup
//     {
//         public Startup(IConfiguration configuration)
//         {
//             Configuration = configuration;
//         }

//         public IConfiguration Configuration { get; }

//         public void ConfigureServices(IServiceCollection services)
//         {
//             services.AddControllers()
//                     .AddNewtonsoftJson(options =>
//                     {
//                         options.SerializerSettings.ContractResolver = new DefaultContractResolver
//                         {
//                             NamingStrategy = new SnakeCaseNamingStrategy()
//                         };
//                     });

//             services.AddDbContext<ApplicationDbContext>(options =>
//                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

//             // Register the DatabaseService class
//             services.AddScoped<DatabaseService>();








//             // Add CORS configuration
//             // services.AddCors(options =>
//             // {
//             // options.AddPolicy("AllowSpecificOrigins",
//             //     builder =>
//             //     {
//             //     builder.WithOrigins("http://localhost:4200")
//             //                             .AllowAnyHeader()
//             //                             .AllowAnyMethod()
//             //                             .AllowCredentials();


//             //     });
//             // });
//             services.AddAuthorization();

//         }

//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             if (env.IsDevelopment())
//             {
//                 app.UseDeveloperExceptionPage();
//             }

//             app.UseHttpsRedirection();
//             app.UseStaticFiles();
//             app.UseRouting();

//             // Add CORS middleware
//             app.Use(async (context, next) =>
//             {
//                 context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
//                 context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
//                 context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
//                 await next();
//             });

//             // Enable CORS
//             app.UseCors("AllowSpecificOrigins");
//             app.UseAuthorization();
//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapControllers().RequireCors("AllowSpecificOrigins");

//             });
//         }



//     }
// }


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Serialization;

namespace projnet
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
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        };
                    });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register the DatabaseService class
            services.AddScoped<DatabaseService>();

            // Add CORS configuration
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularOrigins",
                builder =>
                {
                    builder.WithOrigins(
                                        "http://localhost:4200"
                                        )
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });
            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Enable CORS
            app.UseCors("AllowAngularOrigins");

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

