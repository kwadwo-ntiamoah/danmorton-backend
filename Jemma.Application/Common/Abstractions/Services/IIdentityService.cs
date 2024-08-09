using Jemma.Domain.Aggregates;

namespace Jemma.Application.Common.Abstractions.Services
{
    public interface IIdentityService
    {
        public Task<AuthResult?> LoginAsync(string username, string password);
        public Task<User?> AddUserAsync(string username, string fullName, string role);
        public Task<User> UpdateUserAsync(string username, string newRole, string newName);
        public Task<List<User>> GetUsersAsync();
        public Task DeleteUserAsync(string username);
        public Task ChangePasswordAsync(string username, string currentPassword, string newPassword);
    }

    public record AuthResult(string Token, string FullName, string Role);
}