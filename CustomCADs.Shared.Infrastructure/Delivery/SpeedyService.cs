using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints.Enums;
using CustomCADs.Shared.Speedy.Enums;
using CustomCADs.Shared.Speedy.Services.Calculation;
using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Print;
using CustomCADs.Shared.Speedy.Services.Shipment;
using Microsoft.Extensions.Options;

namespace CustomCADs.Shared.Infrastructure.Delivery;

public sealed class SpeedyService(
    IOptions<DeliverySettings> settings,
    ShipmentService shipmentService,
    CalculationService calculationService,
    PrintService printService
) : IDeliveryService
{
    private readonly AccountModel account = new(settings.Value.Username, settings.Value.Password);

    public async Task<ShipmentDto> ShipAsync(string package, string contents, int parcelCount, double totalWeight, CancellationToken ct = default)
    {
        var response = await shipmentService.CreateShipmentAsync(
            account: account,
            package: package,
            contents: contents,
            parcelCount: parcelCount,
            totalWeight: totalWeight,
            payer: Payer.RECIPIENT,
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
    public async Task<CalculationDto[]> CalculateAsync(string shipmentId, CancellationToken ct = default)
    {
        var response = await calculationService.CalculateAsync(
            account: account,
            shipmentId: shipmentId,
            ct: ct
        ).ConfigureAwait(false);

        return [.. response.Select(c => new CalculationDto(
            ServiceId: c.ServiceId,
            Price: new(
                Amount: c.Price.Amount,
                Vat: c.Price.Vat,
                Total: c.Price.Total,
                Currency: c.Price.Currency
            ),
            PickupDate: c.PickupDate,
            DeliveryDeadline: c.DeliveryDeadline
        ))];
    }

    public async Task<byte[]> PrintAsync(string shipmentId, CancellationToken ct = default)
    {
        byte[] waybill = await printService.PrintAsync(
            account: account,
            shipmentId: shipmentId,
            paperSize: PaperSize.A4,
            ct: ct
        ).ConfigureAwait(false);

        return waybill;
    }
}
