namespace CustomCADs.Catalog.Domain.Products.ValueObjects;

public record Cad(
    string Path,
    Coordinates CamCoordinates,
    Coordinates PanCoordinates);
