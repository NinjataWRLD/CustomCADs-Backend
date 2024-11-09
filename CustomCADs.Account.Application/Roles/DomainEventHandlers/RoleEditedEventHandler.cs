using CustomCADs.Account.Domain.DomainEvents.Roles;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Account.Application.Roles.DomainEventHandlers;

public class RoleEditedEventHandler(ICacheService cache)
{
    public async Task Handle(RoleEditedEvent reEvent)
    {
        await cache.RemoveAsync<IEnumerable<Role>>($"roles").ConfigureAwait(false);
        await cache.SetRangeAsync(
            ($"roles/{reEvent.Id}", reEvent.Role), 
            ($"roles/{reEvent.Role.Name}", reEvent.Role)
        ).ConfigureAwait(false);
    }
}
