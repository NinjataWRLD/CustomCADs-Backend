namespace CustomCADs.Speedy.Http.Endpoints.PickupEndpoints.Pickup;

using Dtos.PickupOrder;

internal record PickupResponse(
	PickupOrderDto[] Orders,
	ErrorDto? Error
);
