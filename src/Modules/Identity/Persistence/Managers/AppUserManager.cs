using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Identity.Persistence.ShadowEntities;
using CustomCADs.Shared.Core.Common.TypedIds.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Identity.Persistence.Managers;

public class AppUserManager(UserManager<AppUser> manager) : IUserManager
{
	private IQueryable<AppUser> Users => manager.Users.Include(x => x.RefreshTokens);

	public async Task<User?> GetByIdAsync(UserId id)
	{
		AppUser? appUser = await Users.FirstOrDefaultAsync(x => x.Id == id.Value).ConfigureAwait(false);
		if (appUser is null)
		{
			return null;
		}

		string role = await GetRoleAsync(appUser).ConfigureAwait(false);
		return appUser?.ToUser(role);
	}

	public async Task<User?> GetByUsernameAsync(string username)
	{
		AppUser? appUser = await Users.FirstOrDefaultAsync(x => x.UserName == username).ConfigureAwait(false);
		if (appUser is null)
		{
			return null;
		}

		string role = await GetRoleAsync(appUser).ConfigureAwait(false);
		return appUser?.ToUser(role);
	}

	public async Task<User?> GetByEmailAsync(string email)
	{
		AppUser? appUser = await Users.FirstOrDefaultAsync(x => x.Email == email).ConfigureAwait(false);
		if (appUser is null)
		{
			return null;
		}

		string role = await GetRoleAsync(appUser).ConfigureAwait(false);
		return appUser?.ToUser(role);
	}

	public async Task<(User? User, RefreshToken? RefreshToken)> GetByRefreshTokenAsync(string token)
	{
		AppUser? appUser = await Users.FirstOrDefaultAsync(x => x.RefreshTokens.Any(x => x.Value == token)).ConfigureAwait(false);
		if (appUser is null)
		{
			return (User: null, RefreshToken: null);
		}

		AppRefreshToken? rt = appUser.RefreshTokens.FirstOrDefault(x => x.Value == token);
		string role = await GetRoleAsync(appUser).ConfigureAwait(false);

		return (appUser?.ToUser(role), rt?.ToRefreshToken(role));
	}

	public async Task<DateTimeOffset?> GetIsLockedOutAsync(string username)
	{
		AppUser? appUser = await manager.FindByNameAsync(username).ConfigureAwait(false);
		if (appUser is null)
		{
			return null;
		}

		bool isLockedOut = await manager.IsLockedOutAsync(appUser).ConfigureAwait(false);
		if (!isLockedOut)
		{
			return null;
		}

		return appUser.LockoutEnd;
	}

	public async Task<bool> AddAsync(User user, string password)
	{
		AppUser appUser = user.ToAppUser();
		await manager.CreateAsync(appUser, password).ConfigureAwait(false);

		var result = await manager.AddToRoleAsync(appUser, user.Role).ConfigureAwait(false);
		return result.Succeeded;
	}

	public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
	{
		AppUser? appUser = await manager.FindByNameAsync(user.Username).ConfigureAwait(false);
		if (appUser is null)
		{
			return string.Empty;
		}

		return await manager.GenerateEmailConfirmationTokenAsync(appUser).ConfigureAwait(false);
	}

	public async Task<bool> ConfirmEmailAsync(User user, string token)
	{
		AppUser? appUser = await manager.FindByNameAsync(user.Username).ConfigureAwait(false);
		if (appUser is null)
		{
			return false;
		}

		var result = await manager.ConfirmEmailAsync(appUser, token).ConfigureAwait(false);
		return result.Succeeded;
	}

	public async Task<string> GeneratePasswordResetTokenAsync(User user)
	{
		AppUser? appUser = await manager.FindByNameAsync(user.Username).ConfigureAwait(false);
		if (appUser is null)
		{
			return string.Empty;
		}

		return await manager.GeneratePasswordResetTokenAsync(appUser).ConfigureAwait(false);
	}

	public async Task<bool> ResetPasswordAsync(User user, string token, string newPassword)
	{
		AppUser? appUser = await manager.FindByNameAsync(user.Username).ConfigureAwait(false);
		if (appUser is null)
		{
			return false;
		}

		var result = await manager.ResetPasswordAsync(appUser, token, newPassword).ConfigureAwait(false);
		return result.Succeeded;
	}

	public async Task<bool> CheckPasswordAsync(User user, string password)
	{
		AppUser? appUser = await manager.FindByNameAsync(user.Username).ConfigureAwait(false);
		if (appUser is null)
		{
			return false;
		}

		bool success = await manager.CheckPasswordAsync(appUser, password).ConfigureAwait(false);
		if (success)
		{
			await manager.ResetAccessFailedCountAsync(appUser).ConfigureAwait(false);
		}
		else
		{
			await manager.AccessFailedAsync(appUser).ConfigureAwait(false);
		}

		return success;
	}

	public async Task UpdateUsernameAsync(UserId id, string username)
	{
		AppUser? appUser = await manager.FindByIdAsync(id.Value.ToString()).ConfigureAwait(false);
		if (appUser is null)
		{
			return;
		}

		appUser.UserName = username;
		await manager.UpdateAsync(appUser).ConfigureAwait(false);
	}

	public async Task UpdateRefreshTokensAsync(UserId id, RefreshToken[] refreshTokens)
	{
		AppUser? appUser = await manager.FindByIdAsync(id.Value.ToString()).ConfigureAwait(false);
		if (appUser is null)
		{
			return;
		}

		appUser.FillRefreshTokens([.. refreshTokens.Select(r => r.ToAppRefreshToken())]);
		await manager.UpdateAsync(appUser).ConfigureAwait(false);
	}

	public async Task DeleteAsync(string username)
	{
		AppUser? appUser = await manager.FindByNameAsync(username).ConfigureAwait(false);
		if (appUser is null)
		{
			return;
		}

		await manager.DeleteAsync(appUser).ConfigureAwait(false);
	}

	private async Task<string> GetRoleAsync(AppUser appUser)
	{
		string[] roles = [.. await manager.GetRolesAsync(appUser).ConfigureAwait(false)];
		return roles.Single();
	}
}
