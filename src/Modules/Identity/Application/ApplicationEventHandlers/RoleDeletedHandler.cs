using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Identity.Domain.Repositories;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.ApplicationEventHandlers;

public class RoleDeletedHandler(IRoleRepository repository)
{
    public async Task Handle(RoleDeletedApplicationEvent ae)
    {
        AppRole role = await repository.GetByNameAsync(ae.Name).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ByName(ae.Name);

        repository.Remove(role);
        await repository.SaveChangesAsync().ConfigureAwait(false);
    }
}
