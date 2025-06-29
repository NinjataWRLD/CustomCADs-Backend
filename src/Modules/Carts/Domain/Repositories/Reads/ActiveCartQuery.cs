﻿using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Domain.Repositories.Reads;

public record ActiveCartQuery(
	Pagination Pagination,
	ProductId? ProductId = null
);
