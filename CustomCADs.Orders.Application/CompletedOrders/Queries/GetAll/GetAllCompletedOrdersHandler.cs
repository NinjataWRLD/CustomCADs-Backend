using CustomCADs.Orders.Domain.CompletedOrders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.GetAll;

public sealed class GetAllCompletedOrdersHandler(ICompletedOrderReads reads, IRequestSender sender)
    : IQueryHandler<GetAllCompletedOrdersQuery, Result<GetAllCompletedOrdersDto>>
{
    public async Task<Result<GetAllCompletedOrdersDto>> Handle(GetAllCompletedOrdersQuery req, CancellationToken ct)
    {
        CompletedOrderQuery query = new(
            Delivery: req.Delivery,
            BuyerId: req.BuyerId,
            DesignerId: req.DesignerId,
            Name: req.Name,
            Sorting: req.Sorting,
            Pagination: req.Pagination
        );
        Result<CompletedOrder> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        AccountId[] buyerIds = [.. result.Items.Select(o => o.BuyerId)];
        AccountId[] designerIds = [.. result.Items.Select(o => o.DesignerId)];

        GetUsernamesByIdsQuery designerUsernamesQuery = new(designerIds);
        Dictionary<AccountId, string> designers = await sender
            .SendQueryAsync(designerUsernamesQuery, ct).ConfigureAwait(false);

        GetUsernamesByIdsQuery buyerUsernamesQuery = new(buyerIds);
        Dictionary<AccountId, string> buyers = await sender
            .SendQueryAsync(buyerUsernamesQuery, ct).ConfigureAwait(false);

        GetTimeZonesByIdsQuery timeZonesQuery = new(buyerIds);
        Dictionary<AccountId, string> timeZones = await sender
            .SendQueryAsync(timeZonesQuery, ct).ConfigureAwait(false);

        return new(
            result.Count,
            result.Items.Select(o => o.ToGetAllOrdersItem(
                buyerUsername: buyers[o.BuyerId],
                designerUsername: designers[o.DesignerId],
                timeZone: timeZones[o.BuyerId]
            )).ToArray()
        );
    }
}
