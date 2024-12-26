using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.Orders.Commands.Remove;

using static Constants.Roles;

public sealed class RemoveOrderHandler(IOrderReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<RemoveOrderCommand>
{
    public async Task Handle(RemoveOrderCommand req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, ct: ct)
            ?? throw OrderNotFoundException.ById(req.Id);

        GetUserRoleByIdQuery userRoleQuery = new(req.RemoverId);
        string role = await sender.SendQueryAsync(userRoleQuery, ct).ConfigureAwait(false);

        if (role != Admin)
        {
            throw OrderAuthorizationException.UnauthorizedOrderRemoval();
        }
        order.SetRemovedStatus();

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
