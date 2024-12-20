using CustomCADs.Shared.Application.Delivery.Dtos;

namespace CustomCADs.Shared.Application.Delivery;

public interface IDeliveryService
{
    Task<CalculationDto[]> CalculateAsync(string shipmentId, CancellationToken ct = default);
    Task<ShipmentDto> ShipAsync(string package, string contents, int parcelCount, double totalWeight, CancellationToken ct = default);
}
