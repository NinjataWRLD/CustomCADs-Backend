using CustomCADs.Catalog.Application.Products.Enums;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetSortings;

public class GetProductDesignerSortingsHandler
	: IQueryHandler<GetProductDesignerSortingsQuery, string[]>
{
	public Task<string[]> Handle(GetProductDesignerSortingsQuery req, CancellationToken ct)
		=> Task.FromResult(
				Enum.GetNames<ProductDesignerSortingType>()
			);
}
