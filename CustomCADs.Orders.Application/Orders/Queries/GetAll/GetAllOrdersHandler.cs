using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.Orders.Queries.GetAll;

public sealed class GetAllOrdersHandler(IOrderReads reads, IRequestSender sender)
    : IQueryHandler<GetAllOrdersQuery, Result<GetAllOrdersDto>>
{
    public async Task<Result<GetAllOrdersDto>> Handle(GetAllOrdersQuery req, CancellationToken ct)
    {
        OrderQuery query = new(
            Delivery: req.Delivery,
            OrderStatus: req.OrderStatus,
            BuyerId: req.BuyerId,
            DesignerId: req.DesignerId,
            Name: req.Name,
            Sorting: req.Sorting,
            Pagination: req.Pagination
        );
        Result<Order> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        AccountId[] buyerIds = [.. result.Items.Select(o => o.BuyerId)];
        AccountId[] designerIds = [.. result.Items
            .Where(o => o.DesignerId is not null)
            .Select(o => o.DesignerId!.Value)
        ];

        GetUsernamesByIdsQuery designerUsernamesQuery = new(designerIds);
        (AccountId Id, string Username)[] designers = await sender
            .SendQueryAsync(designerUsernamesQuery, ct).ConfigureAwait(false);

        GetUsernamesByIdsQuery buyerUsernamesQuery = new(buyerIds);
        (AccountId Id, string Username)[] buyers = await sender
            .SendQueryAsync(buyerUsernamesQuery, ct).ConfigureAwait(false);

        GetTimeZonesByIdsQuery timeZonesQuery = new(buyerIds);
        IEnumerable<(AccountId Id, string TimeZone)> timeZones = await sender
            .SendQueryAsync(timeZonesQuery, ct).ConfigureAwait(false);

        return new(
            result.Count,
            result.Items.Select(o => o.ToGetAllOrdersItem(
                buyerUsername: buyers.Single(d => d.Id == o.BuyerId).Username,
                designerUsername: designers.Single(d => d.Id == o.DesignerId).Username,
                timeZone: timeZones.Single(d => d.Id == o.BuyerId).TimeZone
            )).ToArray()
        );
    }
}
