using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.UseCases.Users.Queries;

namespace CustomCADs.Orders.Application.Orders.Queries.GetAll;

public class GetAllOrdersHandler(IOrderReads reads, IRequestSender sender)
    : IQueryHandler<GetAllOrdersQuery, GetAllOrdersDto>
{
    public async Task<GetAllOrdersDto> Handle(GetAllOrdersQuery req, CancellationToken ct)
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
        OrderResult result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        UserId[] buyerIds = [.. result.Orders.Select(o => o.BuyerId)];
        UserId[] designerIds = [
            .. result.Orders
            .Where(o => o.DesignerId is not null)
            .Select(o => o.DesignerId!.Value)
        ];

        GetUsernamesByIdsQuery designerUsernamesQuery = new(designerIds);
        GetUsernamesByIdsQuery buyerUsernamesQuery = new(buyerIds);
        
        var designersInfo = await sender.SendQueryAsync(designerUsernamesQuery, ct).ConfigureAwait(false);
        var buyersInfo = await sender.SendQueryAsync(buyerUsernamesQuery, ct).ConfigureAwait(false);        

        return new(
            result.Count,
            result.Orders
                .Select(o =>
                {
                    var (_, buyerUsername) = buyersInfo.FirstOrDefault(d => d.Id == o.DesignerId);
                    var (_, designerUsername) = designersInfo.FirstOrDefault(d => d.Id == o.DesignerId);

                    return o.ToGetAllOrdersItem(buyerUsername, designerUsername);
                })
                .ToArray()
        );
    }
}
