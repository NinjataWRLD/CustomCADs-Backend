﻿using CustomCADs.Files.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Files.Domain.Repositories.Reads;

public record CadQuery(
	Pagination Pagination,
	CadId[]? Ids = null,
	CadSorting? Sorting = null
);
