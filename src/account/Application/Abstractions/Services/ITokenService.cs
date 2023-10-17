using Domain;

namespace Application.Abstractions.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(ApplicationUser applicationUser);
    }
}
