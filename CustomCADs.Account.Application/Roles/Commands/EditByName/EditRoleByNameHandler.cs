using CustomCADs.Account.Domain.DomainEvents.Roles;
using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Account.Application.Roles.Commands.EditByName;

public class EditRoleByNameHandler(IRoleReads reads, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<EditRoleByNameCommand>
{
    public async Task Handle(EditRoleByNameCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByNameAsync(req.Name, ct: ct).ConfigureAwait(false)
            ?? throw new RoleNotFoundException(req.Name, new { });

        role.Name = req.Dto.Name;
        role.Description = req.Dto.Description;

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseAsync(new RoleEditedDomainEvent(
            Id: role.Id, 
            Role: role
        )).ConfigureAwait(false);
    }
}