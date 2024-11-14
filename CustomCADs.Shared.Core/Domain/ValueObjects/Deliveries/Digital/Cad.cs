namespace CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Digital;

public record Cad(string Path, Coordinates CamCoordinates, Coordinates PanCoordinates)
{
    public Cad() : this(string.Empty, new(), new()) { }
}
