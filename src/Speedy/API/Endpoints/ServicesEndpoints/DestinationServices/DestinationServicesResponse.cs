namespace CustomCADs.Speedy.API.Endpoints.ServicesEndpoints.DestinationServices;

using Dtos.ExtendedCourierService;

public record DestinationServicesResponse(
	ExtendedCourierServiceDto[] Services,
	ErrorDto? Error
);
