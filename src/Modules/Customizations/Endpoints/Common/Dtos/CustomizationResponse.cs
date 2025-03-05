namespace CustomCADs.Customizations.Endpoints.Common.Dtos;

public record CustomizationResponse(
    Guid Id,
    decimal Scale,
    decimal Infill,
    decimal Volume,
    double Weight,
    decimal Cost,
    string Color,
    int MaterialId
);
