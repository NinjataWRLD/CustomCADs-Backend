using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Accounts.Application.Roles.DomainEventHandlers;

public class RoleDeletedEventHandler(ICacheService cache)
{
    public async Task Handle(RoleDeletedDomainEvent de)
    {
        await cache.RemoveAsync<IEnumerable<Role>>($"roles").ConfigureAwait(false);
        await cache.RemoveRangeAsync<Role>(
            $"roles/{de.Id}",
            $"roles/{de.Name}"
        ).ConfigureAwait(false);
    }
}
