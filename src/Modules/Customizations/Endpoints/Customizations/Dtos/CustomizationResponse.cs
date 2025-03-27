namespace CustomCADs.Customizations.Endpoints.Customizations.Dtos;

public record CustomizationResponse(
    Guid Id,
    decimal Scale,
    decimal Infill,
    double Weight,
    decimal Cost,
    string Color,
    int MaterialId
);
