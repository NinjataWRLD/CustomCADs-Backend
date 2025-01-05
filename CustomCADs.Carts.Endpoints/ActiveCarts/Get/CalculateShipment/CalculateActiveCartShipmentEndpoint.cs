using CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Get.CalculateShipment;

public class CalculateActiveCartShipmentEndpoint(IRequestSender sender)
    : Endpoint<CalculateActiveCartShipmentRequest, ICollection<CalculateActiveCartShipmentResponse>>
{
    public override void Configure()
    {
        Get("calculate/{id}");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("07. Calculate Shipment")
            .WithDescription("Calculate the estimted price for the delivery of the Shipment")
        );
    }

    public override async Task HandleAsync(CalculateActiveCartShipmentRequest req, CancellationToken ct)
    {
        CalculateActiveCartShipmentQuery query = new(
            Id: new ActiveCartId(req.Id),
            Address: new(req.Country, req.City)
        );
        CalculateActiveCartShipmentDto[] calculations = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        ICollection<CalculateActiveCartShipmentResponse> response =
            [.. calculations.Select(c => c.ToCalculateCartShipmentResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
