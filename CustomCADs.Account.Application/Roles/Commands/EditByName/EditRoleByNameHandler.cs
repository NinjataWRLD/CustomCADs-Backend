using CustomCADs.Account.Domain.Common;
using CustomCADs.Account.Domain.Roles.DomainEvents;
using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Account.Application.Roles.Commands.EditByName;

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

        await raiser.RaiseAsync(new RoleEditedDomainEvent(
            Id: role.Id,
            Role: role
        )).ConfigureAwait(false);
    }
}