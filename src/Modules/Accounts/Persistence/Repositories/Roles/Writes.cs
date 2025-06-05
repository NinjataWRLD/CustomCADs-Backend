using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Accounts.Domain.Roles;

namespace CustomCADs.Accounts.Persistence.Repositories.Roles;

public class Writes(AccountsContext context) : IRoleWrites
{
	public async Task<Role> AddAsync(Role entity, CancellationToken ct = default)
		=> (await context.Roles.AddAsync(entity, ct).ConfigureAwait(false)).Entity;

	public void Remove(Role entity)
		=> context.Roles.Remove(entity);
}
