using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Files.Domain.Repositories.Reads;

public record ImageQuery(
	Pagination Pagination,
	ImageId[]? Ids = null
);
