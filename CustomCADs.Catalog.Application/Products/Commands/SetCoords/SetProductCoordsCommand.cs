using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Digital;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;
public record SetProductCoordsCommand(
    ProductId Id,
    Coordinates? CamCoordinates = default,
    Coordinates? PanCoordinates = default
) : ICommand;
