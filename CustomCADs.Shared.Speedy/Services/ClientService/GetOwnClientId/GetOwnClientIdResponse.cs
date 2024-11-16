namespace CustomCADs.Shared.Speedy.Services.ClientService.GetOwnClientId;

public record GetOwnClientIdResponse(
    long ClientId,
    ErrorDto? Error
);