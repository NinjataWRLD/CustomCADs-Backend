using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Queries.Users;

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
        
        UserId[] ids = result.Orders
            .Where(o => o.DesignerId is not null)
            .Select(o => o.DesignerId!.Value)
            .ToArray();

        GetUsernamesByIdsQuery usernamesQuery = new(ids);
        var designersInfo = await sender.SendQueryAsync(usernamesQuery, ct).ConfigureAwait(false);

        return new(
            result.Count, 
            result.Orders
                .Select(o =>
                {
                    var (_, Username) = designersInfo.First(d => d.Id == o.DesignerId);
                    return o.ToGetAllOrdersItem(Username);
                })
                .ToArray()
        );
    }
}
