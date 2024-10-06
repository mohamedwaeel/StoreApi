using Microsoft.AspNetCore.Identity;
using Store.Data.Contexts;
using Store.Data.Entities.IdenetityEntities;
using System.Runtime.CompilerServices;

namespace Store.Web.Extensions
{
    public static class IdentityServiceExtenstion
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<AppUser>();


            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthorization();
            return services;
        }
    }
}
