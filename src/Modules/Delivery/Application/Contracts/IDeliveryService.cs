using CustomCADs.Delivery.Application.Contracts.Dtos;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;

namespace CustomCADs.Delivery.Application.Contracts;

public interface IDeliveryService
{
	Task<CalculationDto[]> CalculateAsync(CalculateRequest req, CancellationToken ct = default);
	Task<ShipmentDto> ShipAsync(ShipRequestDto req, CancellationToken ct = default);
	Task CancelAsync(string shipmentId, string comment, CancellationToken ct = default);
	Task<ShipmentStatusDto[]> TrackAsync(string shipmentId, CancellationToken ct = default);
	Task<byte[]> PrintAsync(string shipmentId, CancellationToken ct = default);
}
