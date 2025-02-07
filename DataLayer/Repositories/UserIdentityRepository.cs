using BusinessLayer;
using BusinessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class UserIdentityRepository
    {
        SportAgencyDbContext context;
        UserManager<User> userManager;

        public UserIdentityRepository(SportAgencyDbContext context,
            UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        #region  Seeding Data with project(Make first profile ADMIN)

        public async Task SeedDataAsync(string adminEmail, string adminPassword)
        {
            int userRoles = await context.UserRoles.CountAsync();
            if (userRoles == 0)
            {
                await ConfigureAdminAccountAsync(adminEmail, adminPassword);
            }
        }

        public async Task ConfigureAdminAccountAsync(string email, string password)
        {
            User adminIdentityUser = await context.Users.FirstAsync();
            if (adminIdentityUser != null)
            {
                //MAKE HIM ADMIN
                await userManager.AddToRoleAsync(adminIdentityUser, Role.Admin.ToString());
                await userManager.AddPasswordAsync(adminIdentityUser, password);
                await userManager.SetEmailAsync(adminIdentityUser, email);
            }
        }
        #endregion Seeding

        #region CRUD
        public async Task<Tuple<IdentityResult, User>> CreateUserAsync(string username, string password, string email
            , string phoneNumber, Role role)
        {
            try
            {
                User user = new User(username, email, phoneNumber, role);
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    return new Tuple<IdentityResult, User>(result, user);
                }
                if (role == Role.Admin)
                {
                    await userManager.AddToRoleAsync(user, Role.Admin.ToString());
                }
                else if (role == Role.Athlete)
                {
                    await userManager.AddToRoleAsync(user, Role.Athlete.ToString());
                }
                else if (role == Role.Club)
                {
                    await userManager.AddToRoleAsync(user, Role.Club.ToString());
                }

                return new Tuple<IdentityResult, User>
                        (IdentityResult.Success, user);
            }

            catch (Exception ex)
            {
                IdentityError er =
                    new IdentityError()
                    {
                        Code = "Registration",
                        Description = ex.Message
                    };
                IdentityResult result =
                    IdentityResult.Failed(er);

                return new Tuple<IdentityResult, User>
                    (result, null);
            }
        }

        public async Task CompleteRegistrationByRole(string userId, object additionalData)
        {
            User user = await context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.UserRole == Role.Athlete && additionalData is Athlete athleteData)
            {
                await context.Users.AddAsync(athleteData);
            }
            else if (user.UserRole == Role.Club && additionalData is Club clubData)
            {
                await context.Users.AddAsync(clubData);
            }
        }

        public async Task<User> LogInUserAsync(string username, string password)
        {
            try
            {
                User userFromDb = await userManager.FindByNameAsync(username);

                if (userFromDb == null)
                {
                    return null;
                }
                else
                {
                    IdentityResult result = await userManager.PasswordValidators[0].ValidateAsync(userManager, userFromDb, password);

                    if (result.Succeeded)
                    {
                        return await context.Users.FindAsync(userFromDb.Id);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> ReadUserAsync(string key, bool NavigationalProporties = false)
        {

            try
            {
                IQueryable<User> query = context.Users;
                if (!NavigationalProporties)
                {
                    return await query.FirstOrDefaultAsync(u => u.Id == key);
                }
                else
                {
                    return await query.Include(u => u.AthleteAds)
                                 .Include(u => u.ClubAds)
                                 .FirstOrDefaultAsync(u => u.Id == key);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ICollection<User>> ReadAllUsersAsync(bool NavigationalProporties = false)
        {

            try
            {
                IQueryable<User> query = context.Users;
                if (!NavigationalProporties)
                {
                    return await context.Users.ToListAsync();
                }
                else
                {
                    return await query.Include(u => u.AthleteAds)
                                 .Include(u => u.ClubAds)
                                 .ToListAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateUserAsync(string id, string username)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    //?
                    User user = await userManager.FindByIdAsync(id);

                    if (user != null)
                    {
                        user.UserName = username;

                        await userManager.UpdateAsync(user);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task DeleteUserByNameAsync(string username)
        {
            try
            {
                User user = await FindUserByNameAsync(username);

                if (user == null)
                {
                    throw new InvalidOperationException("User not found to be deleted!");
                }

                await userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> FindUserByNameAsync(string username)
        {
            try
            {
                // Identity return Null if there is no user!
                return await userManager.FindByNameAsync(username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion Crud
    }
}