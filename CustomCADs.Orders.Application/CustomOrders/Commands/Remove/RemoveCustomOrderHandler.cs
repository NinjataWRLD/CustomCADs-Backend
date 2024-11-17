using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;
using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Queries.Users;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Remove;

using static Constants.Roles;

public class RemoveCustomOrderHandler(ICustomOrderReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<RemoveCustomOrderCommand>
{
    public async Task Handle(RemoveCustomOrderCommand req, CancellationToken ct)
    {
        CustomOrder order = await reads.SingleByIdAsync(req.Id, ct: ct)
            ?? throw CustomOrderNotFoundException.ById(req.Id);

        GetUserRoleByIdQuery userRoleQuery = new(req.RemoverId);
        string role = await sender.SendQueryAsync(userRoleQuery, ct).ConfigureAwait(false);
        
        if (role != Admin)
        {
            throw CustomOrderValidationException.Custom("A User cannot remove an order if he isn't an Admin.");
        }
        order.SetRemovedStatus();

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
