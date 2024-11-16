namespace CustomCADs.Shared.Speedy.Services.ServicesService.Services;

using Dtos.CourierService;
using Dtos.Errors;

public record ServicesResponse(
    CourierServiceDto[] Services,
    ErrorDto? Error
);