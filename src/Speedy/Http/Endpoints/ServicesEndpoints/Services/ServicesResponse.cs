namespace CustomCADs.Speedy.Http.Endpoints.ServicesEndpoints.Services;

using Dtos.CourierService;

internal record ServicesResponse(
	CourierServiceDto[] Services,
	ErrorDto? Error
);
