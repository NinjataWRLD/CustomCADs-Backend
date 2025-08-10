using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Carts.Domain.Repositories.Reads;

public record ActiveCartQuery(
	Pagination Pagination,
	ProductId? ProductId = null
);
