
using BusinessLayer;
using BusinessLayer.Entities;
using DataLayer;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SeedingDB
{
    class Program
    {
        static async Task Main(string[] args)
        {

            try
            {
                IdentityOptions options = new IdentityOptions();
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;

                DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
                builder.UseSqlServer(
                    "Server=KOSTOF31;Database=SportAgencyD;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
                    );

                SportAgencyDbContext dbContext = new SportAgencyDbContext(builder.Options);
                UserManager<User> userManager = new UserManager<User>(
                    new UserStore<User>(dbContext), Options.Create(options),
                    new PasswordHasher<User>(), new List<IUserValidator<User>>() { new UserValidator<User>() },
                    new List<IPasswordValidator<User>>() { new PasswordValidator<User>() }, new UpperInvariantLookupNormalizer(),
                    new IdentityErrorDescriber(), new ServiceCollection().BuildServiceProvider(),
                new Logger<UserManager<User>>(new LoggerFactory())
                );


                UserIdentityRepository userIdentityRepository = new UserIdentityRepository(dbContext, userManager);

                if (!await dbContext.Roles.AnyAsync())
                {
                    await dbContext.Roles.AddRangeAsync(new List<IdentityRole>
                     {
                     new IdentityRole("Admin") { NormalizedName = "ADMIN" },
                     new IdentityRole("Athlete") { NormalizedName = "ATHLETE" },
                     new IdentityRole("Club") { NormalizedName = "CLUB" }
                     });

                    await dbContext.SaveChangesAsync();
                }


                Tuple<IdentityResult, User> result = await userIdentityRepository.CreateUserAsync("admin", "admin", "admincho@abv.bg", "31313131", Role.Admin);

                Console.WriteLine("Roles added successfully!");

                if (result.Item1.Succeeded)
                {
                    Console.WriteLine("Admin account added successfully!");
                }
                else
                {
                    Console.WriteLine("Admin account failed to be created!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }
    }
}

