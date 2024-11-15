using CustomCADs.Shared.Speedy.Dtos.Errors;
using CustomCADs.Shared.Speedy.Dtos.PickupOrder;

namespace CustomCADs.Shared.Speedy.Services.PickupService.Pickup;

public record PickupResponse(
    PickupOrderDto[] Orders,
    ErrorDto? Error
);
