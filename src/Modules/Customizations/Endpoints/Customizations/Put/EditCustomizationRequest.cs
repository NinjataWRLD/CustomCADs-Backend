namespace CustomCADs.Customizations.Endpoints.Customizations.Put;

public record EditCustomizationRequest(
    Guid Id,
    decimal Scale,
    decimal Infill,
    decimal Volume,
    string Color,
    int MaterialId
);
