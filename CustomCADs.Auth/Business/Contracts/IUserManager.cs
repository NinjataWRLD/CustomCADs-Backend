using CustomCADs.Auth.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Business.Contracts;

public interface IUserManager
{
    Task<AppUser?> FindByIdAsync(Guid id);
    Task<AppUser?> FindByNameAsync(string username);
    Task<AppUser?> FindByEmailAsync(string email);
    Task<string> GetRoleAsync(AppUser user);
    Task<bool> IsLockedOutAsync(AppUser user);
    Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
    Task<string> GeneratePasswordResetTokenAsync(AppUser user);
    Task<IdentityResult> CreateAsync(AppUser user);
    Task<IdentityResult> CreateAsync(AppUser user, string password);
    Task UpdateAsync(AppUser user);
    Task RevokeRefreshTokenAsync(Guid id);
    Task UpdateRefreshTokenAsync(Guid id, string rt, DateTime endDate);
    Task AddToRoleAsync(AppUser user, string role);
    Task RemoveFromRoleAsync(AppUser user, string oldRole);
    Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token);
    Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword);
    Task DeleteAsync(AppUser user);
}
