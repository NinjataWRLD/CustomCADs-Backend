namespace CustomCADs.Shared.Speedy.API.Endpoints.ValidationEndpoints.ValidatePostCode;

public record ValidatePostCodeRequest(
    string UserName,
    string Password,
    string Number,
    string? Language,
    long? ClientSystemId,
    string? Ext
);
