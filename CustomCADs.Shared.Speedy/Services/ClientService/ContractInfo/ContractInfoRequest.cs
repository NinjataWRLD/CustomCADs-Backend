namespace CustomCADs.Shared.Speedy.Services.ClientService.ContractInfo;

public record ContractInfoRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);