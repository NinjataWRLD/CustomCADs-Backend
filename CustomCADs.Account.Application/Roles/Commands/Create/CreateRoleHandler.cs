using CustomCADs.Account.Application.Common.Contracts;
using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Shared.Core.Events.Roles;
using Wolverine;

namespace CustomCADs.Account.Application.Roles.Commands.Create;

public class CreateRoleHandler(IWrites<Role> writes, IUnitOfWork uow, IMessageBus bus)
    : ICommandHandler<CreateRoleCommand, int>
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

        RoleCreatedEvent rcEvent = new(req.Dto.Name, req.Dto.Description);
        await bus.PublishAsync(rcEvent).ConfigureAwait(false);

        return role.Id;
    }
}
