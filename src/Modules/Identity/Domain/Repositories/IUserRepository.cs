using CustomCADs.Identity.Domain.Entities;

namespace CustomCADs.Identity.Domain.Repositories;

public interface IUserRepository
{
    Task<AppUser?> GetByIdAsync(Guid id);
    Task<AppUser?> GetByUsernameAsync(string username);
    Task<AppUser?> GetByEmailAsync(string email);
    Task<AppUser?> GetByRefreshTokenAsync(string token);
    Task<string> GetRoleAsync(AppUser user);
    Task<bool> GetIsLockedOutAsync(AppUser user);
    Task AddAsync(string role, AppUser user, string password);
    Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
    Task<bool> ConfirmEmailAsync(AppUser user, string token);
    Task<string> GeneratePasswordResetTokenAsync(AppUser user);
    Task ResetPasswordAsync(AppUser user, string token, string newPassword);
    Task<bool> CheckPasswordAsync(AppUser user, string password);
    void Remove(AppUser user);
    Task SaveChangesAsync();
}
