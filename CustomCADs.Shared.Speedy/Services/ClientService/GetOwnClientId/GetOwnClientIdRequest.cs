namespace CustomCADs.Shared.Speedy.Services.ClientService.GetOwnClientId;

public record GetOwnClientIdRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);