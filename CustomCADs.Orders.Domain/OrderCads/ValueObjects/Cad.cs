namespace CustomCADs.Orders.Domain.OrderCads.ValueObjects;

public record Cad(
    string Path,
    Coordinates CamCoordinates,
    Coordinates PanCoordinates
)
{
    public Cad() : this(string.Empty, new(), new()) { }
}
