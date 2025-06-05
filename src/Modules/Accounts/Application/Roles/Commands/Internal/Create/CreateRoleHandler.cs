using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;

public sealed class CreateRoleHandler(IRoleWrites writes, IUnitOfWork uow, IEventRaiser raiser)
	: ICommandHandler<CreateRoleCommand, RoleId>
{
	public async Task<RoleId> Handle(CreateRoleCommand req, CancellationToken ct)
	{
		Role role = req.Dto.ToEntity();
		await writes.AddAsync(role, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseDomainEventAsync(new RoleCreatedDomainEvent(
			Role: role
		)).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(new RoleCreatedApplicationEvent(
			Name: req.Dto.Name,
			Description: req.Dto.Description
		)).ConfigureAwait(false);

		return role.Id;
	}
}
