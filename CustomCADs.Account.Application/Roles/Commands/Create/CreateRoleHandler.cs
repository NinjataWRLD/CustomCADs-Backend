using CustomCADs.Account.Domain.Shared;
using CustomCADs.Shared.Core.Events;
using CustomCADs.Shared.IntegrationEvents.Account;

namespace CustomCADs.Account.Application.Roles.Commands.Create;

public class CreateRoleHandler(IWrites<Role> writes, IUnitOfWork uow, IEventRaiser raiser)
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
        await raiser.PublishAsync(rcEvent).ConfigureAwait(false);

        return role.Id;
    }
}
