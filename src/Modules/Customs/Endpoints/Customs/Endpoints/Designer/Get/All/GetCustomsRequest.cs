using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Get.All;

public sealed record GetCustomsRequest(
    bool? ForDelivery = null,
    string? Name = null,
    CustomSortingType SortingType = CustomSortingType.OrderedAt,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
