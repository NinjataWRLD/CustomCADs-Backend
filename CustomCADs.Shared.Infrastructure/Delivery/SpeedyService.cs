using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.Speedy.Services.Shipment;
using Microsoft.Extensions.Options;

namespace CustomCADs.Shared.Infrastructure.Delivery;

public sealed class SpeedyService(
    IOptions<DeliverySettings> settings,
    ShipmentService shipmentService
) : IDeliveryService
{
    public async Task<ShipmentDto> ShipAsync(string package, string contents, int parcelCount, int totalWeight, CancellationToken ct = default)
    {
        var response = await shipmentService.CreateShipmentAsync(
            account: new(settings.Value.Username, settings.Value.Password),
            package: package,
            contents: contents,
            parcelCount: parcelCount,
            totalWeight: totalWeight,
            ct: ct
        ).ConfigureAwait(false);

        return new(
            Id: response.Id,
            ParcelIds: [.. response.Parcels.Select(p => p.Id)],
            Price: Convert.ToDecimal(response.Price.Amount),
            PickupDate: response.PickupDate,
            DeliveryDeadline: response.DeliveryDeadline
        );
    }
}
