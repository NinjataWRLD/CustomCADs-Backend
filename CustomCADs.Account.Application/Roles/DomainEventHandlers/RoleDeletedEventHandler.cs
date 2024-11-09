using CustomCADs.Account.Domain.DomainEvents.Roles;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Account.Application.Roles.DomainEventHandlers;

public class RoleDeletedEventHandler(ICacheService cache)
{
    public async Task Handle(RoleDeletedEvent rdEvent)
    {
        await cache.RemoveAsync<IEnumerable<Role>>($"roles").ConfigureAwait(false);
        await cache.RemoveRangeAsync<Role>(
            $"roles/{rdEvent.Id}", 
            $"roles/{rdEvent.Name}"
        ).ConfigureAwait(false);
    }
}
