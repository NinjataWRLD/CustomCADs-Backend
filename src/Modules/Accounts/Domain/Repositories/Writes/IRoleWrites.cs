using CustomCADs.Accounts.Domain.Roles;

namespace CustomCADs.Accounts.Domain.Repositories.Writes;

public interface IRoleWrites
{
	Task<Role> AddAsync(Role entity, CancellationToken ct = default);
	void Remove(Role entity);
}
