using Jemma.Application.Common.Abstractions.Services;
using Jemma.Domain.Aggregates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Jemma.Infrastructure.Services
{
    public class IdentityService(UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager, IJwtService _jwtService) : IIdentityService
    {
        public async Task<User?> AddUserAsync(string username, string fullName, string role)
        {
            // get user
            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
            {
                var newUser = new User
                {
                    FullName = fullName,
                    Email = username,
                    UserName = username,
                    Role = role
                };

                var res = await _userManager.CreateAsync(newUser, username.Split("@")[0] + "@123J"); //jojo@gmail.com => jojo@123J

                if (res.Succeeded)
                {
                    // create role if not existent
                if (!await _roleManager.RoleExistsAsync(newUser.Role))
                    await _roleManager.CreateAsync(new IdentityRole(newUser.Role!));

                // add user to role group if role exists
                if (await _roleManager.RoleExistsAsync(newUser.Role!))
                    await _userManager.AddToRoleAsync(newUser, newUser.Role!);

                    return newUser;
                }

                return null;
            }

            return null;
        }

        public async Task ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(username);

            if (user != null) {
                await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            }
        }

        public async Task DeleteUserAsync(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        public async Task<AuthResult?> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var token = _jwtService.GenerateToken(username, [user.Role]);
                return new AuthResult(token, user.FullName, user.Role);
            }

            return null;
        }

        public async Task<User> UpdateUserAsync(string username, string newRole, string newName)
        {
            var user = await _userManager.FindByEmailAsync(username);

            if (user != null) {
                user.FullName = newName;
                user.Role = newRole;

                await _userManager.UpdateAsync(user);

                return user;
            }

            return new User { Email = username, UserName = username };
        }
    }
}