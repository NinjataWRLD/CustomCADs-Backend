using CustomCADs.Accounts.Application.Roles.Exceptions;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Accounts.Application.Roles.Commands.EditByName;

public class EditRoleByNameHandler(IRoleReads reads, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<EditRoleByNameCommand>
{
    public async Task Handle(EditRoleByNameCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByNameAsync(req.Name, ct: ct).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ByName(req.Name);

        role.SetName(req.Dto.Name);
        role.SetDescription(req.Dto.Description);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new RoleEditedDomainEvent(
            Id: role.Id,
            Role: role
        )).ConfigureAwait(false);
    }
}