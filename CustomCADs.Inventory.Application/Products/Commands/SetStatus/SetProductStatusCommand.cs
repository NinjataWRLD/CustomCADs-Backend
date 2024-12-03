using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Inventory.Application.Products.Commands.SetStatus;

public sealed record SetProductStatusCommand(
    ProductId Id,
    ProductStatus Status,
    AccountId DesignerId
) : ICommand;
