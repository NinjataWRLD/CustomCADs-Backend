using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Inventory.Application.Products.Commands.SetCoords;

public record SetProductCoordsCommand(
    ProductId Id,
    UserId CreatorId,
    Coordinates? CamCoordinates = default,
    Coordinates? PanCoordinates = default
) : ICommand;
