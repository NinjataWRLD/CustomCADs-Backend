namespace CustomCADs.Shared.Speedy.Services.PickupService.Pickup;

using Dtos.PickupOrder;

public record PickupResponse(
    PickupOrderDto[] Orders,
    ErrorDto? Error
);
