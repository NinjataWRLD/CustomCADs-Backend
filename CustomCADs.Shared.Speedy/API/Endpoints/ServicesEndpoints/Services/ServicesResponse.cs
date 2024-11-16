namespace CustomCADs.Shared.Speedy.API.Endpoints.ServicesEndpoints.Services;

using Dtos.CourierService;

public record ServicesResponse(
    CourierServiceDto[] Services,
    ErrorDto? Error
);