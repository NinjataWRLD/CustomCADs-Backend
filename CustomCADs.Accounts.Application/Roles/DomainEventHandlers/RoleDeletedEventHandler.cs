using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Accounts.Application.Roles.DomainEventHandlers;

public class RoleDeletedEventHandler(ICacheService cache)
{
    public async Task Handle(RoleDeletedDomainEvent de)
    {
        await cache.RemoveRolesArrayAsync().ConfigureAwait(false);
        await cache.RemoveRoleAsync(de.Id).ConfigureAwait(false);
        await cache.RemoveRoleAsync(de.Name).ConfigureAwait(false);
    }
}
