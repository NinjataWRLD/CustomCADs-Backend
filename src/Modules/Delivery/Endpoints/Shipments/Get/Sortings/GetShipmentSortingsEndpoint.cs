using CustomCADs.Delivery.Application.Shipments.Queries.GetSortings;

namespace CustomCADs.Delivery.Endpoints.Shipments.Get.Sortings;

public sealed class GetShipmentSortingsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<string[]>
{
    public override void Configure()
    {
        Get("sortings");
        Group<ShipmentsGroup>();
        Description(d => d
            .WithSummary("05. Sortings")
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
