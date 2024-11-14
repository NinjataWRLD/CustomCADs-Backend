using CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request;
using CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Response;
using Refit;

namespace CustomCADs.Shared.Speedy.ShipmentService;

public interface IShipmentService
{
    [Post("")]
    Task<CreateShipmentResponse> CreateShipment(CreateShipmentRequest request, CancellationToken ct = default);
}
