namespace CustomCADs.Shared.Speedy.Services.ClientService.GetOwnClientId;

using Dtos.Errors;

public record GetOwnClientIdResponse(
    long ClientId,
    ErrorDto? Error
);