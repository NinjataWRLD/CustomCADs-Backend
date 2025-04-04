using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Identity.Domain.Entities;
using CustomCADs.Identity.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Identity.Infrastructure.Repositories;

public class AppUserRepository(IdentityContext context, UserManager<AppUser> manager) : IUserRepository
{
    public async Task<AppUser?> GetByIdAsync(Guid id)
        => await context.Users.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

    public async Task<AppUser?> GetByUsernameAsync(string username)
        => await context.Users.FirstOrDefaultAsync(x => x.UserName == username).ConfigureAwait(false);

    public async Task<AppUser?> GetByEmailAsync(string email)
        => await context.Users.FirstOrDefaultAsync(u => u.Email == email).ConfigureAwait(false);

    public async Task<AppUser?> GetByRefreshTokenAsync(string token)
        => await context.Users.FirstOrDefaultAsync(u => u.RefreshToken == token).ConfigureAwait(false);

    public async Task<string> GetRoleAsync(AppUser user)
    {
        string[] roles = [.. await manager.GetRolesAsync(user).ConfigureAwait(false)];
        return roles.Single();
    }

    public async Task<bool> GetIsLockedOutAsync(AppUser user)
        => await manager.IsLockedOutAsync(user).ConfigureAwait(false);

    public async Task AddAsync(string role, AppUser user, string password)
    {
        var result = await manager.CreateAsync(user, password).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            throw UserCreationException.ByUsername(user.UserName ?? string.Empty);
        }

        IdentityResult roleResult = await manager.AddToRoleAsync(user, role).ConfigureAwait(false);
        if (!roleResult.Succeeded)
        {
            throw UserCreationException.WithRole(user.UserName ?? string.Empty, role);
        }
    }
    
    public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        => await manager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);

    public async Task<bool> ConfirmEmailAsync(AppUser user, string token)
    {
        var result = await manager.ConfirmEmailAsync(user, token).ConfigureAwait(false);
        return result.Succeeded;
    }

    public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        => await manager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);

    public async Task ResetPasswordAsync(AppUser user, string token, string newPassword)
    {
        var result = await manager.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            throw UserPasswordException.ResetFailure(user.UserName ?? string.Empty);
        }
    }

    public async Task<bool> CheckPasswordAsync(AppUser user, string password)
    {
        bool success = await manager.CheckPasswordAsync(user, password).ConfigureAwait(false);

        if (success)
            await manager.ResetAccessFailedCountAsync(user).ConfigureAwait(false);
        else
            await manager.AccessFailedAsync(user).ConfigureAwait(false);

        return success;
    }

    public void Remove(AppUser user)
        => context.Users.Remove(user);

    public async Task SaveChangesAsync()
        => await context.SaveChangesAsync().ConfigureAwait(false);
}
