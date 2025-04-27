using BusinessLayer;
using BusinessLayer.Entities;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.Contexts
{
    public class UserIdentityContext
    {
        private readonly UserIdentityRepository repository;
        public UserIdentityContext(UserIdentityRepository repository)
        {
            this.repository = repository;
        }

        #region Operations
        public async Task<Tuple<IdentityResult, User>> CreateUserAsync(string username, string password, string email, string phoneNumber, Role role)
        {
            return await repository.CreateUserAsync(username, password, email, phoneNumber, role);
        }

        public async Task CompleteRegistrationByRole(string userId, object additionalData)
        {
            await repository.CompleteRegistrationByRole(userId, additionalData);
        }

        public async Task<User> LogInUserAsync(string username, string password)
        {
            return await repository.LogInUserAsync(username, password);
        }

        public async Task<User> ReadUserAsync(string key, bool NavigationalProporties = false)
        {
            return await repository.ReadUserAsync(key, NavigationalProporties);
        }

        public async Task<ICollection<User>> ReadAllUsersAsync(bool NavigationalProporties = false)
        {
            return await repository.ReadAllUsersAsync(NavigationalProporties);
        }

        public async Task UpdateUserAsync(string id, string username, string email, string phoneNumber, Role role)
        {
            await repository.UpdateUserAsync(id, username, email, phoneNumber, role);
        }

        public async Task DeleteUserByNameAsync(string username)
        {
            await repository.DeleteUserByNameAsync(username);
        }

        public async Task<User> FindUserByNameAsync(string username)
        {
            return await repository.FindUserByNameAsync(username);
        }

        public async Task<User> FindUserByIdAsync(string id)
        {
            return await repository.FindUserByIdAsync(id);
        }

        #endregion
    }
}
