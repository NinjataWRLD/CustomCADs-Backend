using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Application.Products.Commands.Delete;

public record DeleteProductCommand(
    ProductId Id,
    UserId CreatorId
) : ICommand;