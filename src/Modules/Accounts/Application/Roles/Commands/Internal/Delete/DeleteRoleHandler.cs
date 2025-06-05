using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Accounts.Application.Roles.Commands.Internal.Delete;

public sealed class DeleteRoleHandler(IRoleReads reads, IRoleWrites writes, IUnitOfWork uow, IEventRaiser raiser)
	: ICommandHandler<DeleteRoleCommand>
{
	public async Task Handle(DeleteRoleCommand req, CancellationToken ct)
	{
		Role role = await reads.SingleByNameAsync(req.Name, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Role>.ByProp(nameof(req.Name), req.Name);

		writes.Remove(role);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseDomainEventAsync(new RoleDeletedDomainEvent(
			Id: role.Id,
			Name: role.Name
		)).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(new RoleDeletedApplicationEvent(
			Name: role.Name
		)).ConfigureAwait(false);
	}
}
