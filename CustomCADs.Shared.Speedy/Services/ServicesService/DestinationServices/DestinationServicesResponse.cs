namespace CustomCADs.Shared.Speedy.Services.ServicesService.DestinationServices;

using Dtos.ExtendedCourierService;

public record DestinationServicesResponse(
    ExtendedCourierServiceDto[] Services,
    ErrorDto? Error
);