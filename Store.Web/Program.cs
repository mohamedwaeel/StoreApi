
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Data.Contexts;
using Store.Repository;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.HandleResponse;
using Store.Service.Services.ProductServices;
using Store.Service.Services.ProductServices.Dtos;
using Store.Web.Helper;
using Store.Web.Middleware;
using Store.Web.Extensions;
using StackExchange.Redis;
namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args) // Change void to Task
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });



            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var configurations = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(configurations);
            }
            );

            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityService(builder.Configuration);


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerDocumentation();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200", "https://localhost:7153");
                });
            });
            var app = builder.Build();

            await ApplySeeding.ApplySeedingAsync(app); // Properly await the async operation

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CarsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync(); // Use RunAsync for an async entry point
        }
    }
}
