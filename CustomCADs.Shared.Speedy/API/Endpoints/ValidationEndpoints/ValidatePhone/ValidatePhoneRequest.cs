namespace CustomCADs.Shared.Speedy.API.Endpoints.ValidationEndpoints.ValidatePhone;

public record ValidatePhoneRequest(
    string UserName,
    string Password,
    int? CountryId,
    long? SiteId,
    string? Language,
    long? ClientSystemId
);
