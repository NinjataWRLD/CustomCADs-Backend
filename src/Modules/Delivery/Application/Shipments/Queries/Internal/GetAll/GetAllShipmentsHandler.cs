using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;

public class GetAllShipmentsHandler(IShipmentReads reads, IRequestSender sender)
    : IQueryHandler<GetAllShipmentsQuery, Result<GetAllShipmentsDto>>
{
    public async Task<Result<GetAllShipmentsDto>> Handle(GetAllShipmentsQuery req, CancellationToken ct)
    {
        ShipmentQuery query = new(
            CustomerId: req.CustomerId,
            Sorting: req.Sorting,
            Pagination: req.Pagination
        );
        Result<Shipment> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        AccountId[] buyerIds = [.. result.Items.Select(i => i.BuyerId)];
        Dictionary<AccountId, string> buyers = await sender
            .SendQueryAsync(new GetUsernamesByIdsQuery(buyerIds), ct).ConfigureAwait(false);

        return new(
            Count: result.Count,
            Items: [.. result.Items.Select(i => i.ToGetAllDto(buyers[i.BuyerId]))]
        );
    }
}
