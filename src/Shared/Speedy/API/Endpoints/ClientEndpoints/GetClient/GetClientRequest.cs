namespace CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints.GetClient;

public record GetClientRequest(
    string UserName,
    string Password,
    string? Language,
    long? ClientSystemId
);