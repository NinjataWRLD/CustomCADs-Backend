using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Identity.Persistence.ShadowEntities;
using CustomCADs.Shared.Core.Common.TypedIds.Identity;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Persistence.Repositories.Users;

public class Writes(UserManager<AppUser> manager) : IUserWrites
{
	public async Task<bool> CreateAsync(User user, string password)
	{
		AppUser appUser = user.ToAppUser();
		await manager.CreateAsync(appUser, password).ConfigureAwait(false);

		var result = await manager.AddToRoleAsync(appUser, user.Role).ConfigureAwait(false);
		return result.Succeeded;
	}

	public async Task<string> GenerateEmailConfirmationTokenAsync(string username)
	{
		AppUser? appUser = await manager.FindByNameAsync(username).ConfigureAwait(false);
		if (appUser is null)
		{
			return string.Empty;
		}

		return await manager.GenerateEmailConfirmationTokenAsync(appUser).ConfigureAwait(false);
	}

	public async Task<bool> ConfirmEmailAsync(string username, string token)
	{
		AppUser? appUser = await manager.FindByNameAsync(username).ConfigureAwait(false);
		if (appUser is null)
		{
			return false;
		}

		var result = await manager.ConfirmEmailAsync(appUser, token).ConfigureAwait(false);
		return result.Succeeded;
	}

	public async Task<string> GeneratePasswordResetTokenAsync(string username)
	{
		AppUser? appUser = await manager.FindByNameAsync(username).ConfigureAwait(false);
		if (appUser is null)
		{
			return string.Empty;
		}

		return await manager.GeneratePasswordResetTokenAsync(appUser).ConfigureAwait(false);
	}

	public async Task<bool> ResetPasswordAsync(string username, string token, string newPassword)
	{
		AppUser? appUser = await manager.FindByNameAsync(username).ConfigureAwait(false);
		if (appUser is null)
		{
			return false;
		}

		var result = await manager.ResetPasswordAsync(appUser, token, newPassword).ConfigureAwait(false);
		return result.Succeeded;
	}

	public async Task<bool> CheckPasswordAsync(string username, string password)
	{
		AppUser? appUser = await manager.FindByNameAsync(username).ConfigureAwait(false);
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
}
