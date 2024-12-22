using CustomCADs.Shared.Speedy.API.Endpoints.CalculationEndpoints;
using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Models.Calculation;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Content;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Payment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Recipient;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Shared.Speedy.Services.Shipment;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Services.Calculation;

public class CalculationService(
    ICalculationEndpoints endpoints,
    ShipmentService shipmentService
)
{
    public async Task<(int ServiceId, ShipmentAdditionalServicesModel AdditionalServices, ShipmentPriceModel Price, DateOnly PickupDate, string DeliveryDeadline)[]> CalculateAsync(
        AccountModel account,
        string shipmentId,
        CancellationToken ct = default)
    {
        var shipments = await shipmentService.ShipmentInfoAsync(account, [shipmentId], ct).ConfigureAwait(false);
        var shipment = shipments.Single();

        var response = await endpoints.Calculation(new(
            UserName: account.Username,
            Password: account.Password,
            Location: account.Language,
            ClientSystemId: account.ClientSystemId,
            Recipient: shipment.Recipient.ToCalculation(),
            Sender: shipment.Sender.ToCalculation(),
            Service: shipment.Service.ToCalculation(),
            Content: shipment.Content.ToCalculation(),
            Payment: shipment.Payment.ToDto()
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Calculations.Select(c => c.ToModel())];
    }
}
