using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Catalog.Application.Products.Commands.SetCoords;

public record SetProductCoordsCommand(
    ProductId Id,
    UserId CreatorId,
    Coordinates? CamCoordinates = default,
    Coordinates? PanCoordinates = default
) : ICommand;
