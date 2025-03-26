using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Accounts.Application.Roles.EventHandlers.Domain;

public class RoleDeletedEventHandler(ICacheService cache)
{
    public async Task Handle(RoleDeletedDomainEvent de)
    {
        await cache.RemoveRolesArrayAsync().ConfigureAwait(false);
        await cache.RemoveRoleAsync(de.Id).ConfigureAwait(false);
        await cache.RemoveRoleAsync(de.Name).ConfigureAwait(false);
    }
}
