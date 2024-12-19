using CustomCADs.Shared.Application.Delivery.Dtos;

namespace CustomCADs.Shared.Application.Delivery;

public interface IDeliveryService
{
    Task<ShipmentDto> ShipAsync(string package, string contents, int parcelCount, double totalWeight, CancellationToken ct = default);
}
