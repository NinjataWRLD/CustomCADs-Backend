using CustomCADs.Identity.Domain.Repositories;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.ApplicationEventHandlers;

public class RoleCreatedHandler(IRoleRepository repository)
{
    public async Task Handle(RoleCreatedApplicationEvent ae)
    {
        AppRole role = new(ae.Name);

        await repository.AddAsync(role).ConfigureAwait(false);
        await repository.SaveChangesAsync().ConfigureAwait(false);
    }
}
