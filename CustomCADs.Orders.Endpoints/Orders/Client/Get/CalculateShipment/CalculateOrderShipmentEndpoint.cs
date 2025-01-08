using CustomCADs.Orders.Application.Orders.Queries.CalculateShipment;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Get.CalculateShipment;

public class CalculateOrderShipmentEndpoint(IRequestSender sender)
    : Endpoint<CalculateOrderShipmentRequest, ICollection<CalculateOrderShipmentResponse>>
{
    public override void Configure()
    {
        Get("calculate/{id}");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("10. Calculate Shipment")
            .WithDescription("Calculate the estimted price for the delivery of the Shipment")
        );
    }

    public override async Task HandleAsync(CalculateOrderShipmentRequest req, CancellationToken ct)
    {
        CalculateOrderShipmentQuery query = new(
            Id: OrderId.New(req.Id),
            TotalWeight: req.Weight,
            Address: new(req.Country, req.City)
        );
        CalculateOrderShipmentDto[] calculations = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        ICollection<CalculateOrderShipmentResponse> response =
            [.. calculations.Select(c => c.ToCalculateOrderShipmentResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
