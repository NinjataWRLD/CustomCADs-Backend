namespace CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints.GetContactByExternalId;

public record GetContactByExternalIdRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId,
    string? SecretKey
);