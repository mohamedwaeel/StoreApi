using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdenetityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync (UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Mohamed wael",
                    Email = "wael@gmail.com",
                    UserName = "MohamedWaell",
                    Address = new Address
                    {
                        FirstName = "mohamed",
                        LastName = "Wael",
                        City = "Alexandria",
                        State = "Alex",
                        Street = "3",
                        PostalCode = "12344"
                    }
                };
                await userManager.CreateAsync (user,"Password1234");
            }
        }
    }
}
