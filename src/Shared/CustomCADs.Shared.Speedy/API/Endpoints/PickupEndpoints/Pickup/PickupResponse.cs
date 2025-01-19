namespace CustomCADs.Shared.Speedy.API.Endpoints.PickupEndpoints.Pickup;

using Dtos.PickupOrder;

public record PickupResponse(
    PickupOrderDto[] Orders,
    ErrorDto? Error
);
