namespace CustomCADs.Shared.Speedy.Services.ServicesService.Services;

using Dtos.CourierService;

public record ServicesResponse(
    CourierServiceDto[] Services,
    ErrorDto? Error
);