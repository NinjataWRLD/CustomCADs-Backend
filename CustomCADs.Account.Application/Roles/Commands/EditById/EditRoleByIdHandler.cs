using CustomCADs.Account.Application.Roles.Common.Exceptions;
using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Shared.Domain;

namespace CustomCADs.Account.Application.Roles.Commands.EditById;

public class EditRoleByIdHandler(IRoleReads reads, IUnitOfWork uow)
{
    public async Task Handle(EditRoleByIdCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new RoleNotFoundException($"The Role with id: {req.Id} does not exist.");

        role.Name = req.Dto.Name;
        role.Description = req.Dto.Description;

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
