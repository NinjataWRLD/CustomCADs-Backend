using CustomCADs.Account.Application.Roles.Exceptions;
using CustomCADs.Account.Domain.Common;
using CustomCADs.Account.Domain.Common.Exceptions.Roles;
using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Roles.DomainEvents;
using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Account.Application.Roles.Commands.EditById;

public class EditRoleByIdHandler(IRoleReads reads, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<EditRoleByIdCommand>
{
    public async Task Handle(EditRoleByIdCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ById(req.Id);

        role.SetName(req.Dto.Name);
        role.SetDescription(req.Dto.Description);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new RoleEditedDomainEvent(
            Id: req.Id,
            Role: role
        )).ConfigureAwait(false);
    }
}
