using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetById;

public sealed record DesignerGetProductByIdQuery(
	ProductId Id,
	AccountId DesignerId
) : IQuery<DesignerGetProductByIdDto>;
