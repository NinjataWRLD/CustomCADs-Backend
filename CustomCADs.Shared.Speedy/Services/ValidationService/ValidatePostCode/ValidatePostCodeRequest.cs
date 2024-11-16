namespace CustomCADs.Shared.Speedy.Services.ValidationService.ValidatePostCode;

public record ValidatePostCodeRequest(
    string UserName,
    string Password,
    string Number,
    string? Language,
    long? ClientSystemId,
    string? Ext
);
