using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Files.Domain.Repositories.Reads;

public record ImageQuery(
	Pagination Pagination,
	ImageId[]? Ids = null
);
