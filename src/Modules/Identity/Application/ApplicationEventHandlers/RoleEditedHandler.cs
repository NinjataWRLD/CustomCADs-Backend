using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Identity.Domain.Repositories;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.ApplicationEventHandlers;

public class RoleEditedHandler(IRoleRepository repository)
{
    public async Task Handle(RoleEditedApplicationEvent ae)
    {
        AppRole role = await repository.GetByNameAsync(ae.Name).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ByName(ae.Name);

        role.Name = ae.Name;
        await repository.SaveChangesAsync().ConfigureAwait(false);
    }
}
