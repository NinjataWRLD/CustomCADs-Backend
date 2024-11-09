using CustomCADs.Account.Domain.DomainEvents.Roles;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Account.Application.Roles.DomainEventHandlers;

public class RoleCreatedEventHandler(ICacheService cache)
{
    public async Task Handle(RoleCreatedEvent rcEvent)
    {
        await cache.RemoveAsync<IEnumerable<Role>>($"roles").ConfigureAwait(false);
        await cache.SetRangeAsync(
            ($"roles/{rcEvent.Role.Id}", rcEvent.Role), 
            ($"roles/{rcEvent.Role.Name}", rcEvent.Role)
        ).ConfigureAwait(false);
    }
}
