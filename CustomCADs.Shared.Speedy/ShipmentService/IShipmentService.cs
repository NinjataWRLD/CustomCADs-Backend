using Refit;

namespace CustomCADs.Shared.Speedy.ShipmentService;

using CancelShipment;
using CreateShipment;
using ShipmentInfo;

public interface IShipmentService
{
    [Post("")]
    Task<CreateShipmentResponse> CreateShipment(CreateShipmentRequest request, CancellationToken ct = default);

    [Delete("")]
    Task<CancelShipmentResponse> CancelShipment(CancelShipmentRequest request, CancellationToken ct = default);

    [Post("info")]
    Task<ShipmentInfoResponse> ShipmentInfo(ShipmentInfoRequest request, CancellationToken ct = default);
}
