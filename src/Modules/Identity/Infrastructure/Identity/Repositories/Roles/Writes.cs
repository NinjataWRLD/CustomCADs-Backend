using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Infrastructure.Identity.Repositories.Roles;

public class Writes(RoleManager<AppRole> manager) : IRoleWrites
{
	public async Task CreateAsync(string name)
	{
		AppRole role = new(name);
		await manager.CreateAsync(role).ConfigureAwait(false);
	}

	public async Task<bool> DeleteAsync(string name)
	{
		AppRole? role = await manager.FindByNameAsync(name).ConfigureAwait(false);
		if (role == null)
		{
			return false;
		}

		await manager.DeleteAsync(role).ConfigureAwait(false);
		return true;
	}
}
