namespace CustomCADs.Speedy.Http.Endpoints.ServicesEndpoints.DestinationServices;

using Dtos.ExtendedCourierService;

internal record DestinationServicesResponse(
	ExtendedCourierServiceDto[] Services,
	ErrorDto? Error
);
