﻿using CustomCADs.Shared.Speedy.API.Endpoints.CalculationEndpoints;
using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Models.Calculation;
using CustomCADs.Shared.Speedy.Services.Models.Calculation.Content;
using CustomCADs.Shared.Speedy.Services.Models.Calculation.Recipient;
using CustomCADs.Shared.Speedy.Services.Models.Calculation.Sender;
using CustomCADs.Shared.Speedy.Services.Models.Calculation.Service;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Payment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Services.Calculation;

public class CalculationService(ICalculationEndpoints endpoints)
{
    public async Task<(int ServiceId, ShipmentAdditionalServicesModel AdditionalServices, ShipmentPriceModel Price, DateOnly PickupDate, string DeliveryDeadline)[]> Calculate(
        AccountModel account,
        CalculationRecipientModel recipient,
        CalculationServiceModel service,
        CalculationContentModel content,
        ShipmentPaymentModel payment,
        CalculationSenderModel? sender = null,
        CancellationToken ct = default)
    {
        var response = await endpoints.Calculation(new(
            UserName: account.Username,
            Password: account.Password,
            Location: account.Language,
            ClientSystemId: account.ClientSystemId,
            Recipient: recipient.ToDto(),
            Service: service.ToDto(),
            Content: content.ToDto(),
            Payment: payment.ToDto(),
            Sender: sender?.ToDto()
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Calculations.Select(c => c.ToModel())];
    }
}
