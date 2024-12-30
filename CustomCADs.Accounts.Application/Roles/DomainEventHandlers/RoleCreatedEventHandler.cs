using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Accounts.Application.Roles.DomainEventHandlers;

public class RoleCreatedEventHandler(ICacheService cache)
{
    public async Task Handle(RoleCreatedDomainEvent de)
    {
        await cache.RemoveRolesArrayAsync().ConfigureAwait(false);
        await cache.SetRoleAsync(de.Role.Id, de.Role).ConfigureAwait(false);
        await cache.SetRoleAsync(de.Role.Name, de.Role).ConfigureAwait(false);
    }
}
