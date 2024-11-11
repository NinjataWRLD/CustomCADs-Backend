using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Digital;
using System.ComponentModel.DataAnnotations;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;

using static Constants.Cads;

public record SetProductCoordsCommand(
    ProductId Id,

    [Range(CoordMin, CoordMax)]
    Coordinates? CamCoordinates = default,

    [Range(CoordMin, CoordMax)]
    Coordinates? PanCoordinates = default

) : ICommand;
