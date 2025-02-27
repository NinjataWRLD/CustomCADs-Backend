using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Delete;

public sealed record DeleteProductCommand(
    ProductId Id,
    AccountId CreatorId
) : ICommand;