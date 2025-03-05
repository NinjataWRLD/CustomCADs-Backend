namespace CustomCADs.Customizations.Endpoints.Customizations.Post;

public record CreateCustomizationRequest(
    decimal Scale,
    decimal Infill,
    decimal Volume,
    string Color,
    int MaterialId
);
