using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Identity.Infrastructure.Identity;

public class AppUserService(UserManager<AppUser> manager) : IUserService
{
	#region GetUserByX
	public async Task<User> GetByUsernameAsync(string username)
	{
		AppUser appUser = await manager.Users
			.Include(x => x.RefreshTokens)
			.FirstOrDefaultAsync(x => x.UserName == username)
			.ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);

		return await appUser.ToUserWithRoleAsync(manager).ConfigureAwait(false);
	}

	public async Task<(User User, RefreshToken RefreshToken)> GetByRefreshTokenAsync(string refreshToken)
	{
		AppUser appUser = await manager.Users
			.Include(x => x.RefreshTokens)
			.FirstOrDefaultAsync(x => x.RefreshTokens.Any(x => x.Value == refreshToken))
			.ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(refreshToken), refreshToken);

		return await appUser.ToUserWithRoleAsync(manager, refreshToken).ConfigureAwait(false);
	}
	#endregion

	#region GetPropertyX
	public async Task<AccountId> GetAccountIdAsync(string username)
	{
		AccountId accountId = await manager.Users
			.Where(x => x.UserName == username)
			.Select(x => x.AccountId)
			.FirstOrDefaultAsync()
			.ConfigureAwait(false);

		if (accountId.IsEmpty())
		{
			throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);
		}

		return accountId;
	}

	public async Task<DateTimeOffset?> GetIsLockedOutAsync(string username)
	{
		AppUser appUser = await manager.Users
			.FirstOrDefaultAsync(x => x.UserName == username)
			.ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);

		bool isLockedOut = await manager.IsLockedOutAsync(appUser).ConfigureAwait(false);
		if (!isLockedOut)
		{
			return null;
		}

		return appUser.LockoutEnd;
	}
	#endregion

	#region Lifecycle
	public async Task CreateAsync(User user, string password)
	{
		AppUser appUser = user.ToAppUser();
		await manager.CreateAsync(appUser, password).ConfigureAwait(false);

		IdentityResult result = await manager.AddToRoleAsync(appUser, user.Role).ConfigureAwait(false);
		if (!result.Succeeded)
		{
			throw new CustomException($"Couldn't create an account for: {user.Username}.");
		}
	}

	public async Task DeleteAsync(string username)
	{
		AppUser appUser = await manager.FindByIdAsync(username).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);

		await manager.DeleteAsync(appUser).ConfigureAwait(false);
	}
	#endregion

	#region Mutation
	public async Task<bool> CheckPasswordAsync(string username, string password)
	{
		AppUser appUser = await manager.FindByNameAsync(username).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);

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
		AppUser appUser = await manager.FindByIdAsync(id.ToString()).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(id), id);

		appUser.UserName = username;
		await manager.UpdateAsync(appUser).ConfigureAwait(false);
	}

	public async Task<RefreshToken> AddRefreshTokenAsync(User user, string token, bool longerSession)
	{
		RefreshToken refreshToken = user.AddRefreshToken(token, longerSession);

		AppUser appUser = await manager.FindByIdAsync(user.Id.ToString()).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(user.Id), user.Id);

		appUser.FillRefreshTokens([.. user.RefreshTokens.Select(r => r.ToAppRefreshToken())]);
		await manager.UpdateAsync(appUser).ConfigureAwait(false);

		return refreshToken;
	}

	public async Task RevokeRefreshTokenAsync(string token)
	{
		(User User, RefreshToken RefreshToken) = await GetByRefreshTokenAsync(token).ConfigureAwait(false);
		User.RemoveRefreshToken(RefreshToken);

		AppUser appUser = await manager.FindByIdAsync(User.Id.ToString()).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(User.Id), User.Id);

		appUser.FillRefreshTokens([.. User.RefreshTokens.Select(r => r.ToAppRefreshToken())]);
		await manager.UpdateAsync(appUser).ConfigureAwait(false);
	}
	#endregion

	#region Token Generation
	public async Task<string> GenerateEmailConfirmationTokenAsync(string username)
	{
		AppUser appUser = await manager.FindByNameAsync(username).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);

		return await manager.GenerateEmailConfirmationTokenAsync(appUser).ConfigureAwait(false);
	}

	public async Task ConfirmEmailAsync(string username, string token)
	{
		AppUser appUser = await manager.FindByNameAsync(username).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(username), username);

		IdentityResult result = await manager.ConfirmEmailAsync(appUser, token).ConfigureAwait(false);
		if (!result.Succeeded)
		{
			throw CustomAuthorizationException<User>.Custom($"Error confirming Account: {username}'s email.");
		}
	}

	public async Task<string> GeneratePasswordResetTokenAsync(string email)
	{
		AppUser appUser = await manager.FindByEmailAsync(email).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(email), email);

		return await manager.GeneratePasswordResetTokenAsync(appUser).ConfigureAwait(false);
	}

	public async Task ResetPasswordAsync(string email, string token, string newPassword)
	{
		AppUser appUser = await manager.FindByEmailAsync(email).ConfigureAwait(false)
			?? throw CustomNotFoundException<AppUser>.ByProp(nameof(email), email);

		IdentityResult result = await manager.ResetPasswordAsync(appUser, token, newPassword).ConfigureAwait(false);
		if (!result.Succeeded)
		{
			throw CustomAuthorizationException<User>.Custom($"Failed to reset Account: {email}'s password.");
		}
	}
	#endregion
}
