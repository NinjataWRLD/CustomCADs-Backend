using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Create;

public sealed record CreateProductCommand(
	string Name,
	string Description,
	CategoryId CategoryId,
	string ImageKey,
	string ImageContentType,
	string CadKey,
	string CadContentType,
	decimal CadVolume,
	decimal Price,
	AccountId CreatorId
) : ICommand<ProductId>;
