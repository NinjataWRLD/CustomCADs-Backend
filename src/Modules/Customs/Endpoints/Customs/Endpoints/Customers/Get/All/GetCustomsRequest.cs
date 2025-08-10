using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.All;

public sealed record GetCustomsRequest(
	bool? ForDelivery = null,
	CustomStatus? Status = null,
	string? Name = null,
	CustomSortingType SortingType = CustomSortingType.OrderedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 20
);
