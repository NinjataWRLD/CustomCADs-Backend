using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Catalog.Application.Products.Queries.Shared;

public class GetProductCadIdsByIdsHandler(IProductReads reads)
	: IQueryHandler<GetProductCadIdsByIdsQuery, Dictionary<ProductId, CadId>>
{
	public async Task<Dictionary<ProductId, CadId>> Handle(GetProductCadIdsByIdsQuery req, CancellationToken ct)
	{
		ProductQuery query = new(
			Pagination: new(1, req.Ids.Length),
			Ids: req.Ids
		);
		Result<Product> result = await reads.AllAsync(query, track: false, ct).ConfigureAwait(false);

		return result.Items.ToDictionary(i => i.Id, i => i.CadId);
	}
}
