using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Inventory.Application.Products.Commands.SetCoords;

public sealed record SetProductCoordsCommand(
    ProductId Id,
    AccountId CreatorId,
    CoordinatesDto? CamCoordinates = default,
    CoordinatesDto? PanCoordinates = default
) : ICommand;
