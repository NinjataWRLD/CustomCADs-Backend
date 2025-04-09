using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.CalculateShipment;

public class CalculateCustomShipmentEndpoint(IRequestSender sender)
    : Endpoint<CalculateCustomShipmentRequest, ICollection<CalculateCustomShipmentResponse>>
{
    public override void Configure()
    {
        Get("calculate/{id}");
        Group<CustomerGroup>();
        Description(d => d
            .WithSummary("Calculate Shipment")
            .WithDescription("Calculate the estimted price for the delivery of the Shipment")
        );
    }

    public override async Task HandleAsync(CalculateCustomShipmentRequest req, CancellationToken ct)
    {
        CalculateShipmentDto[] calculations = await sender.SendQueryAsync(
            new CalculateCustomShipmentQuery(
                Id: CustomId.New(req.Id),
                Count: req.Count,
                Address: new(req.Country, req.City),
                CustomizationId: CustomizationId.New(req.CustomizationId)
            ),
            ct
        ).ConfigureAwait(false);

        ICollection<CalculateCustomShipmentResponse> response =
            [.. calculations.Select(c => c.ToResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
