namespace CustomCADs.Shared.Speedy.Services.ServicesService.Services;

public record ServicesRequest(
    string UserName,
    string Password,
    string? Language,
    long? ClientSystemId,
    string? Date
);
