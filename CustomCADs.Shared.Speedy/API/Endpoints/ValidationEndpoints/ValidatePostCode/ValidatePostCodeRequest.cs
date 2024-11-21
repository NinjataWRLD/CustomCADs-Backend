namespace CustomCADs.Shared.Speedy.API.Endpoints.ValidationEndpoints.ValidatePostCode;

public record ValidatePostCodeRequest(
    string UserName,
    string Password,
    string PostCode,
    int? CountryId,
    long? SiteId,
    string? Language,
    long? ClientSystemId
);
