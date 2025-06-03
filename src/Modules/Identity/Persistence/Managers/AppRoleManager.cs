using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Identity.Persistence.ShadowEntities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Persistence.Managers;

public class AppRoleManager(RoleManager<AppRole> manager) : IRoleManager
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
