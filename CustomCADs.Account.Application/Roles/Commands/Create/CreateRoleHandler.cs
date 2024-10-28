using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Shared.Domain;

namespace CustomCADs.Account.Application.Roles.Commands.Create;

public class CreateRoleHandler(IWrites<Role> writes, IUnitOfWork uow)
{
    public async Task<int> Handle(CreateRoleCommand req, CancellationToken ct)
    {
        Role role = new()
        {
            Name = req.Dto.Name,
            Description = req.Dto.Description,
        };

        await writes.AddAsync(role, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return role.Id;
    }
}
