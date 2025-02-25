namespace CustomCADs.Shared.Speedy.API.Endpoints.ValidationEndpoints.ValidatePhone;

public record ValidatePhoneRequest(
    string UserName,
    string Password,
    string Number,
    string? Language,
    long? ClientSystemId,
    string? Ext
);
