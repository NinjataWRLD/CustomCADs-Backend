using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;
public record SetProductCoordsCommand(
    ProductId Id,
    Coordinates? CamCoordinates = default,
    Coordinates? PanCoordinates = default
) : ICommand;
