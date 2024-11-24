using CustomCADs.Inventory.Domain.Products.Enums;

namespace CustomCADs.Inventory.Application.Products.Commands.SetStatus;

public record SetProductStatusCommand(
    ProductId Id,
    ProductStatus Status
) : ICommand;
