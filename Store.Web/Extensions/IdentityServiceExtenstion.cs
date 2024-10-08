using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Store.Data.Contexts;
using Store.Data.Entities.IdenetityEntities;
using System.Runtime.CompilerServices;
using System.Text;

namespace Store.Web.Extensions
{
    public static class IdentityServiceExtenstion
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,IConfiguration _IConfig)
        {
            var builder = services.AddIdentityCore<AppUser>();


            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_IConfig["Token:Key"])),
                    ValidateIssuer=true,
                    ValidIssuer= _IConfig["Token:Issuer"],
                    ValidateAudience=false,
                };
            });
            return services;
        }
    }
}
