using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Accounts.Application.Roles.DomainEventHandlers;

public class RoleEditedEventHandler(ICacheService cache)
{
    public async Task Handle(RoleEditedDomainEvent de)
    {
        await cache.RemoveRolesArrayAsync().ConfigureAwait(false);
        await cache.SetRoleAsync(de.Id, de.Role).ConfigureAwait(false);
        await cache.SetRoleAsync(de.Role.Name, de.Role).ConfigureAwait(false);
    }
}
