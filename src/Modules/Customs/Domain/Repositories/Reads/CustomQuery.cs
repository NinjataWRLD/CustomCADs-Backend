using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Customs.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Customs.Domain.Repositories.Reads;

public record CustomQuery(
	Pagination Pagination,
	bool? ForDelivery = null,
	CustomStatus? CustomStatus = null,
	ProductId? ProductId = null,
	AccountId? BuyerId = null,
	AccountId? DesignerId = null,
	string? Name = null,
	CustomSorting? Sorting = null
);
