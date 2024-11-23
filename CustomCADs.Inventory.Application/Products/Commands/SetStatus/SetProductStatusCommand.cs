using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Application.Products.Commands.SetStatus;

public record SetProductStatusCommand(
    ProductId Id,
    ProductStatus Status
) : ICommand;
