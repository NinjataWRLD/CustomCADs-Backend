using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;

public sealed class CreateRoleHandler(IRoleWrites writes, IUnitOfWork uow, BaseCachingService<RoleId, Role> cache, IEventRaiser raiser)
	: ICommandHandler<CreateRoleCommand, RoleId>
{
	public async Task<RoleId> Handle(CreateRoleCommand req, CancellationToken ct)
	{
		Role role = await writes.AddAsync(
			entity: Role.Create(req.Dto.Name, req.Dto.Description),
			ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(role.Id, role).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(new RoleCreatedApplicationEvent(
			Name: req.Dto.Name,
			Description: req.Dto.Description
		)).ConfigureAwait(false);

		return role.Id;
	}
}
