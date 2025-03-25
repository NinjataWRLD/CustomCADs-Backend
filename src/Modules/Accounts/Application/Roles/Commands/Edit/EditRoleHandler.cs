using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Accounts.Application.Roles.Commands.Edit;

public sealed class EditRoleHandler(IRoleReads reads, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<EditRoleCommand>
{
    public async Task Handle(EditRoleCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByNameAsync(req.Name, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Role>.ByProp(nameof(req.Name), req.Name);

        role
            .SetName(req.Dto.Name)
            .SetDescription(req.Dto.Description);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new RoleEditedDomainEvent(
            Id: role.Id,
            Role: role
        )).ConfigureAwait(false);

        await raiser.RaiseApplicationEventAsync(new RoleEditedApplicationEvent(
            Name: role.Name,
            Description: role.Description
        )).ConfigureAwait(false);
    }
}