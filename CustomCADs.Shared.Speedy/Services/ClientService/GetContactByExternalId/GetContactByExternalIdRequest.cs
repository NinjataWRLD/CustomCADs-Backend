namespace CustomCADs.Shared.Speedy.Services.ClientService.GetContactByExternalId;

public record GetContactByExternalIdRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId,
    string? SecretKey
);