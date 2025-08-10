using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Files.Domain.Repositories.Reads;

public record CadQuery(
	Pagination Pagination,
	CadId[]? Ids = null
);
