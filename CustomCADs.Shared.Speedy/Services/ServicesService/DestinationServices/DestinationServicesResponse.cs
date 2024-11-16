namespace CustomCADs.Shared.Speedy.Services.ServicesService.DestinationServices;

using Dtos.Errors;
using Dtos.ExtendedCourierService;

public record DestinationServicesResponse(
    ExtendedCourierServiceDto[] Services,
    ErrorDto? Error
);