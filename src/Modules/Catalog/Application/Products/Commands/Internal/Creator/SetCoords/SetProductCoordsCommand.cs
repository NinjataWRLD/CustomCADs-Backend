using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetCoords;

public sealed record SetProductCoordsCommand(
	ProductId Id,
	AccountId CreatorId,
	CoordinatesDto? CamCoordinates = default,
	CoordinatesDto? PanCoordinates = default
) : ICommand;
