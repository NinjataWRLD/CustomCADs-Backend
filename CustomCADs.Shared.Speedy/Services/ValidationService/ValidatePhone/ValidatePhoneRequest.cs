namespace CustomCADs.Shared.Speedy.Services.ValidationService.ValidatePhone;

public record ValidatePhoneRequest(
    string UserName,
    string Password,
    int? CountryId,
    long? SiteId,
    string? Language,
    long? ClientSystemId
);
