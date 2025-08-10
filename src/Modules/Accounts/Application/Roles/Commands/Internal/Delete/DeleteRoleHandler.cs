using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Accounts.Application.Roles.Commands.Internal.Delete;

public sealed class DeleteRoleHandler(IRoleReads reads, IRoleWrites writes, IUnitOfWork uow, BaseCachingService<RoleId, Role> cache, IEventRaiser raiser)
	: ICommandHandler<DeleteRoleCommand>
{
	public async Task Handle(DeleteRoleCommand req, CancellationToken ct)
	{
		Role role = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Role>.ById(req.Id)
		).ConfigureAwait(false);

		writes.Remove(role);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(new RoleDeletedApplicationEvent(
			Name: role.Name
		)).ConfigureAwait(false);
	}
}
