using CustomCADs.Auth.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Application.Contracts;

public interface IUserService
{
    Task<AppUser?> FindByIdAsync(Guid id);
    Task<AppUser?> FindByNameAsync(string username);
    Task<AppUser?> FindByEmailAsync(string email);
    Task<AppUser?> FindByRefreshTokenAsync(string rt);
    Task<string> GetRoleAsync(AppUser user);
    Task<bool> IsLockedOutAsync(AppUser user);
    Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
    Task<string> GeneratePasswordResetTokenAsync(AppUser user);
    Task<IdentityResult> CreateAsync(AppUser user);
    Task<IdentityResult> CreateAsync(AppUser user, string password);
    Task<IdentityResult> UpdateAsync(AppUser user);
    Task<IdentityResult> UpdateAccountIdAsync(Guid id, Guid accountId);
    Task<IdentityResult> UpdateAccountIdAsync(string username, Guid accountId);
    Task<IdentityResult> UpdateRefreshTokenAsync(Guid id, string rt, DateTime endDate);
    Task<IdentityResult> RevokeRefreshTokenAsync(Guid id);
    Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
    Task<IdentityResult> RemoveFromRoleAsync(AppUser user, string oldRole);
    Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token);
    Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword);
    Task<IdentityResult> DeleteAsync(AppUser user);
}
