namespace CustomCADs.Shared.UseCases.Products.Queries;

public record GetProductCadIdsByIdsQuery(
	ProductId[] Ids
) : IQuery<Dictionary<ProductId, CadId>>;
