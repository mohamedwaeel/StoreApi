
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Data.Contexts;
using Store.Repository;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.Services.ProductServices;
using Store.Service.Services.ProductServices.Dtos;
using Store.Web.Helper;
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

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddAutoMapper(typeof(ProductProfile));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            await ApplySeeding.ApplySeedingAsync(app); // Properly await the async operation

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync(); // Use RunAsync for an async entry point
        }
    }
}
