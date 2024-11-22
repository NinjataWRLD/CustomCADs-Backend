using CustomCADs.Shared.Application.Delivery.Dtos;

namespace CustomCADs.Shared.Application.Delivery;

public interface IDeliveryService
{
    Task<ShipmentDto> Ship(string package, string contents, int parcelCount, int totalWeight, CancellationToken ct = default);
}
