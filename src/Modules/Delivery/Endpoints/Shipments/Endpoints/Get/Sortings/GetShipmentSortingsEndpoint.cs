using CustomCADs.Delivery.Application.Shipments.Queries.GetSortings;
using CustomCADs.Delivery.Endpoints.Shipments.Endpoints;

namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Sortings;

public sealed class GetShipmentSortingsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<string[]>
{
    public override void Configure()
    {
        Get("sortings");
        Group<ShipmentsGroup>();
        Description(d => d
            .WithSummary("Sortings")
            .WithDescription("See all Shipment Sorting types")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetShipmentSortingsQuery query = new();
        string[] result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await SendOkAsync(result).ConfigureAwait(false);
    }
}
