using CustomCADs.Catalog.Application.Common.Contracts;
using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;

public record SetProductCoordsCommand(
    Guid Id, 
    Coordinates? CamCoordinates = default, 
    Coordinates? PanCoordinates = default
) : ICommand;
