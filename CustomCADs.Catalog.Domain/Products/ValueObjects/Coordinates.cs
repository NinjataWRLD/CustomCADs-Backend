namespace CustomCADs.Catalog.Domain.Products.ValueObjects;

public record Coordinates(int X, int Y, int Z)
{
    public Coordinates() : this(0, 0, 0) { }
}