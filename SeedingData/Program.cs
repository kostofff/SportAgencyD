
using BusinessLayer.Entities;
using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Repositories;

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

                //dbContext.Roles.Add(new IdentityRole("Admin") { NormalizedName = "ADMIN" });
                //dbContext.Roles.Add(new IdentityRole("Athlete") { NormalizedName = "Athlete" });
                //dbContext.Roles.Add(new IdentityRole("Club") { NormalizedName = "Club" });
                //try
                //{
                //    await dbContext.SaveChangesAsync();
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine("Error while saving changes: " + ex.Message);
                //    if (ex.InnerException != null)
                //    {
                //        Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                //    }
                //}


                Tuple<IdentityResult, User> result = await userIdentityRepository.CreateUserAsync("admin1", "admin1", "admincho1@abv.bg", "15879485", Role.Admin);

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

