using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders.Entities;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Queries.Users;

namespace CustomCADs.Orders.Application.Orders.Commands.Remove;

using static Constants.Roles;

public class RemoveOrderHandler(IOrderReads reads, IUnitOfWork uow, IRequestSender sender)
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
            throw OrderValidationException.Custom("A User cannot remove an order if he isn't an Admin.");
        }
        order.SetRemovedStatus();

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
