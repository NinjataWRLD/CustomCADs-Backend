using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Identity.Persistence.ShadowEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Identity.Persistence.Repositories.Users;

public class Reads(UserManager<AppUser> manager) : IUserReads
{
	private IQueryable<AppUser> Users => manager.Users.Include(x => x.RefreshTokens);

	public async Task<User?> GetByUsernameAsync(string username)
	{
		AppUser? appUser = await Users.FirstOrDefaultAsync(x => x.UserName == username).ConfigureAwait(false);
		if (appUser is null)
		{
			return null;
		}

		return appUser?.ToUser(role: await GetRoleAsync(appUser).ConfigureAwait(false));
	}

	public async Task<User?> GetByEmailAsync(string email)
	{
		AppUser? appUser = await Users.FirstOrDefaultAsync(x => x.Email == email).ConfigureAwait(false);
		if (appUser is null)
		{
			return null;
		}

		return appUser?.ToUser(role: await GetRoleAsync(appUser).ConfigureAwait(false));
	}

	public async Task<(User? User, RefreshToken? RefreshToken)> GetByRefreshTokenAsync(string token)
	{
		AppUser? appUser = await Users.FirstOrDefaultAsync(x => x.RefreshTokens.Any(x => x.Value == token)).ConfigureAwait(false);
		if (appUser is null)
		{
			return (User: null, RefreshToken: null);
		}

		return (
			User: appUser.ToUser(role: await GetRoleAsync(appUser).ConfigureAwait(false)),
			RefreshToken: appUser.RefreshTokens.First(x => x.Value == token).ToRefreshToken()
		);
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

	private async Task<string> GetRoleAsync(AppUser appUser)
		=> (await manager.GetRolesAsync(appUser).ConfigureAwait(false)).Single();
}
