using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Accounts.Application.Roles.Events.Domain;

public class RoleCreatedEventHandler(ICacheService cache)
{
    public async Task Handle(RoleCreatedDomainEvent de)
    {
        await cache.RemoveRolesArrayAsync().ConfigureAwait(false);
        await cache.SetRoleAsync(de.Role.Id, de.Role).ConfigureAwait(false);
        await cache.SetRoleAsync(de.Role.Name, de.Role).ConfigureAwait(false);
    }
}
