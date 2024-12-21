using CustomCADs.Delivery.Application.Shipments.Queries.GetAll;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Delivery.Endpoints.Shipments.Get;

public class GetShipmentsEndpoint(IRequestSender sender)
    : Endpoint<GetShipmentsRequest, Result<GetShipmentsResponse>>
{
    public override void Configure()
    {
        Get("");
        Group<ShipmentGroup>();
        Description(d => d
            .WithSummary("01. All")
            .WithDescription("See all your Shipments with Filter, Search, Sorting and Pagination options")
        );
    }

    public override async Task HandleAsync(GetShipmentsRequest req, CancellationToken ct)
    {
        GetAllShipmentsQuery query = new(
            ClientId: User.GetAccountId(),
            ShipmentStatus: req.ShipmentStatus,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        Result<GetAllShipmentsDto> result = await sender.SendQueryAsync(query, ct);

        Result<GetShipmentsResponse> response = new(
            Count: result.Count,
            Items: [.. result.Items.Select(i => i.ToGetShipmentsResponse())]
        );
        await SendAsync(response, cancellation: ct);
    }
}
