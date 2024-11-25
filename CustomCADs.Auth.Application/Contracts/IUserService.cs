using CustomCADs.Auth.Application.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Auth.Application.Contracts;

public interface IUserService
{
    Task<AppUser?> FindByIdAsync(Guid id);
    Task<AppUser?> FindByNameAsync(string username);
    Task<AppUser?> FindByEmailAsync(string email);
    Task<AppUser?> FindByRefreshTokenAsync(string rt);
    Task<string> GetRoleAsync(AppUser user);
    Task<bool> IsLockedOutAsync(AppUser user);
    Task<IdentityResult> CreateUserAsync(CreateUserDto dto);
    Task<IdentityResult> CreateAsync(AppUser user);
    Task<IdentityResult> CreateAsync(AppUser user, string password);
    Task<IdentityResult> UpdateAsync(AppUser user);
    Task<IdentityResult> UpdateAccountIdAsync(Guid id, UserId accountId);
    Task<IdentityResult> UpdateAccountIdAsync(string username, UserId accountId);
    Task<IdentityResult> UpdateRefreshTokenAsync(Guid id, string rt, DateTime endDate);
    Task<IdentityResult> RevokeRefreshTokenAsync(string username);
    Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
    Task<IdentityResult> RemoveFromRoleAsync(AppUser user, string oldRole);
    Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token);
    Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword);
    Task<IdentityResult> DeleteAsync(AppUser user);
    Task SendVerificationEmailAsync(string username);
    Task SendVerificationEmailAsync(AppUser user);
    Task SendResetPasswordEmailAsync(AppUser user);
    Task SendResetPasswordEmailAsync(string email);
    Task<bool> CheckPasswordAsync(AppUser user, string password);
}
