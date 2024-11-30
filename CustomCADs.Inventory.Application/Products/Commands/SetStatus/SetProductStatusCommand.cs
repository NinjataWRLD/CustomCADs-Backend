using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Application.Products.Commands.SetStatus;

public record SetProductStatusCommand(
    ProductId Id,
    ProductStatus Status,
    AccountId CreatorId
) : ICommand;
