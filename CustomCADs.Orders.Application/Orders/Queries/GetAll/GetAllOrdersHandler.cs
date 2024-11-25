using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.UseCases.Users.Queries;

namespace CustomCADs.Orders.Application.Orders.Queries.GetAll;

public class GetAllOrdersHandler(IOrderReads reads, IRequestSender sender)
    : IQueryHandler<GetAllOrdersQuery, Result<GetAllOrdersDto>>
{
    public async Task<Result<GetAllOrdersDto>> Handle(GetAllOrdersQuery req, CancellationToken ct)
    {
        OrderQuery query = new(
            DeliveryType: req.DeliveryType,
            OrderStatus: req.OrderStatus,
            BuyerId: req.BuyerId,
            DesignerId: req.DesignerId,
            Name: req.Name,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        Result<Order> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        UserId[] buyerIds = [.. result.Items.Select(o => o.BuyerId)];
        UserId[] designerIds = [
            .. result.Items
            .Where(o => o.DesignerId is not null)
            .Select(o => o.DesignerId!.Value)
        ];

        GetUsernamesByIdsQuery designerUsernamesQuery = new(designerIds);
        GetUsernamesByIdsQuery buyerUsernamesQuery = new(buyerIds);

        var designersInfo = await sender.SendQueryAsync(designerUsernamesQuery, ct).ConfigureAwait(false);
        var buyersInfo = await sender.SendQueryAsync(buyerUsernamesQuery, ct).ConfigureAwait(false);

        return new(
            result.Count,
            result.Items
                .Select(o =>
                {
                    var (_, BuyerUsername) = buyersInfo.FirstOrDefault(d => d.Id == o.DesignerId);
                    var (_, DesignerUsername) = designersInfo.FirstOrDefault(d => d.Id == o.DesignerId);

                    return o.ToGetAllOrdersItem(BuyerUsername, DesignerUsername);
                })
                .ToArray()
        );
    }
}
