using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "Bob",
                Email = "a@a.pl",
                UserName = "a@a.pl",
                Address = new Address
                {
                    FirstName = "Bob",
                    LastName = "Bobowski",
                    Street = "Street 22",
                    City = "New York",
                    State = "NY",
                    ZipCode = "00-001"
                }
            };

            await userManager.CreateAsync(user, "Asdfasdf1!");
        }
    }
}