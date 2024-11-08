using CustomCADs.Catalog.Domain.Products.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;

using static ProductConstants;

public record SetProductCoordsCommand(

    Guid Id,
    
    [Range(CoordMin, CoordMax)]
    Coordinates? CamCoordinates = default,
    
    [Range(CoordMin, CoordMax)]
    Coordinates? PanCoordinates = default

) : ICommand;
