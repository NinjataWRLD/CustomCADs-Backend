﻿using CustomCADs.Customs.Application.Customs.Queries.Internal.Client.CalculateShipment;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Get.CalculateShipment;

public class CalculateCustomShipmentEndpoint(IRequestSender sender)
    : Endpoint<CalculateCustomShipmentRequest, ICollection<CalculateCustomShipmentResponse>>
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

    public override async Task HandleAsync(CalculateCustomShipmentRequest req, CancellationToken ct)
    {
        CalculateCustomShipmentQuery query = new(
            Id: CustomId.New(req.Id),
            Count: req.Count,
            Address: new(req.Country, req.City),
            CustomizationId: CustomizationId.New(req.CustomizationId)
        );
        CalculateShipmentDto[] calculations = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        ICollection<CalculateCustomShipmentResponse> response =
            [.. calculations.Select(c => c.ToResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
