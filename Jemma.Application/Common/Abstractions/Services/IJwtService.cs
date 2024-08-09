namespace Jemma.Application.Common.Abstractions.Services
{
    public interface IJwtService
    {
        string GenerateToken(string username, List<string> roles);
    }
}