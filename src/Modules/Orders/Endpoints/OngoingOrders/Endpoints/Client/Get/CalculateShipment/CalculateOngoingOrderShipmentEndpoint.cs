﻿using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.CalculateShipment;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.CalculateShipment;

public class CalculateOngoingOrderShipmentEndpoint(IRequestSender sender)
    : Endpoint<CalculateOngoingOrderShipmentRequest, ICollection<CalculateOngoingOrderShipmentResponse>>
{
    public override void Configure()
    {
        Get("calculate/{id}");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Calculate Shipment")
            .WithDescription("Calculate the estimted price for the delivery of the Shipment")
        );
    }

    public override async Task HandleAsync(CalculateOngoingOrderShipmentRequest req, CancellationToken ct)
    {
        CalculateOngoingOrderShipmentQuery query = new(
            Id: OngoingOrderId.New(req.Id),
            Count: req.Count,
            Address: new(req.Country, req.City),
            CustomizationId: CustomizationId.New(req.CustomizationId)
        );
        CalculateShipmentDto[] calculations = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        ICollection<CalculateOngoingOrderShipmentResponse> response =
            [.. calculations.Select(c => c.ToResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
