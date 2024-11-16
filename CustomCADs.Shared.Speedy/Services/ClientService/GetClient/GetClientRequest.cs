namespace CustomCADs.Shared.Speedy.Services.ClientService.GetClient;

public record GetClientRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);