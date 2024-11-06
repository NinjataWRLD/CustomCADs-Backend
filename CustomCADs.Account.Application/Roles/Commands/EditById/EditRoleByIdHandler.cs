using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Account.Domain.Shared;

namespace CustomCADs.Account.Application.Roles.Commands.EditById;

public class EditRoleByIdHandler(IRoleReads reads, IUnitOfWork uow)
    : ICommandHandler<EditRoleByIdCommand>
{
    public async Task Handle(EditRoleByIdCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new RoleNotFoundException(req.Id);

        role.Name = req.Dto.Name;
        role.Description = req.Dto.Description;

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
