using CustomCADs.Carts.Application.Carts.Queries.CalculateShipment;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Carts.Endpoints.Carts.Get.CalculateShipment;

public class CalculateCartShipmentEndpoint(IRequestSender sender)
    : Endpoint<CalculateCartShipmentRequest, ICollection<CalculateCartShipmentResponse>>
{
    public override void Configure()
    {
        Get("calculate/{id}");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("10. Calculate Shipment")
            .WithDescription("Calculate the estimted price for the delivery of the Shipment")
        );
    }

    public override async Task HandleAsync(CalculateCartShipmentRequest req, CancellationToken ct)
    {
        CalculateCartShipmentQuery query = new(
            Id: new CartId(req.Id),
            Address: new(req.Country, req.City)
        );
        CalculateCartShipmentDto[] calculations = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        ICollection<CalculateCartShipmentResponse> response =
            [.. calculations.Select(c => c.ToCalculateCartShipmentResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
