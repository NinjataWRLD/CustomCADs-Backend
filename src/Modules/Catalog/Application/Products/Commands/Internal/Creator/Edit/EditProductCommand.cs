using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Edit;

public sealed record EditProductCommand(
	ProductId Id,
	string Name,
	string Description,
	decimal Price,
	CategoryId CategoryId,
	AccountId CreatorId
) : ICommand;
