using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Application.Exceptions;
using CustomCADs.Auth.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Application.Services;

public class AppUserService(UserManager<AppUser> manager) : IUserService
{
    public async Task<AppUser?> FindByIdAsync(Guid id)
        => await manager.FindByIdAsync(id.ToString()).ConfigureAwait(false);

    public async Task<AppUser?> FindByNameAsync(string username)
        => await manager.FindByNameAsync(username).ConfigureAwait(false);

    public async Task<AppUser?> FindByEmailAsync(string email)
        => await manager.FindByEmailAsync(email).ConfigureAwait(false);

    public async Task<string> GetRoleAsync(AppUser user)
        => (await manager.GetRolesAsync(user).ConfigureAwait(false)).Single();

    public async Task<bool> IsLockedOutAsync(AppUser user)
        => await manager.IsLockedOutAsync(user).ConfigureAwait(false);

    public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        => await manager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);

    public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        => await manager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);

    public async Task<IdentityResult> CreateAsync(AppUser user)
        => await manager.CreateAsync(user).ConfigureAwait(false);

    public async Task<IdentityResult> CreateAsync(AppUser user, string password)
        => await manager.CreateAsync(user, password).ConfigureAwait(false);

    public async Task UpdateAsync(AppUser user)
        => await manager.UpdateAsync(user).ConfigureAwait(false);

    public async Task RevokeRefreshTokenAsync(Guid id)
    {
        AppUser user = await FindByIdAsync(id).ConfigureAwait(false)
            ?? throw new UserNotFoundException(id);

        user.RefreshToken = null;
        user.RefreshTokenEndDate = null;

        await manager.UpdateAsync(user).ConfigureAwait(false);
    }

    public async Task UpdateRefreshTokenAsync(Guid id, string rt, DateTime endDate)
    {
        AppUser user = await FindByIdAsync(id).ConfigureAwait(false)
            ?? throw new UserNotFoundException(id);

        user.RefreshToken = rt;
        user.RefreshTokenEndDate = endDate;

        await manager.UpdateAsync(user).ConfigureAwait(false);
    }

    public async Task AddToRoleAsync(AppUser user, string role)
        => await manager.AddToRoleAsync(user, role).ConfigureAwait(false);

    public async Task RemoveFromRoleAsync(AppUser user, string oldRole)
        => await manager.RemoveFromRoleAsync(user, oldRole).ConfigureAwait(false);

    public async Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token)
        => await manager.ConfirmEmailAsync(user, token).ConfigureAwait(false);

    public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword)
        => await manager.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);

    public async Task DeleteAsync(AppUser user)
        => await manager.DeleteAsync(user).ConfigureAwait(false);
}
