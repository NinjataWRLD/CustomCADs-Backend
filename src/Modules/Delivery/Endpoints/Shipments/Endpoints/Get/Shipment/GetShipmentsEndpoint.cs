using CustomCADs.Delivery.Application.Shipments.Queries.GetAll;
using CustomCADs.Delivery.Endpoints.Shipments.Endpoints;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Shipment;

public class GetShipmentsEndpoint(IRequestSender sender)
    : Endpoint<GetShipmentsRequest, Result<GetShipmentsResponse>>
{
    public override void Configure()
    {
        Get("");
        Group<ShipmentsGroup>();
        Description(d => d
            .WithSummary("All")
            .WithDescription("See all your Shipments with Filter, Search, Sorting and Pagination options")
        );
    }

    public override async Task HandleAsync(GetShipmentsRequest req, CancellationToken ct)
    {
        GetAllShipmentsQuery query = new(
            ClientId: User.GetAccountId(),
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        Result<GetAllShipmentsDto> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetShipmentsResponse> response = new(
            Count: result.Count,
            Items: [.. result.Items.Select(i => i.ToResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
