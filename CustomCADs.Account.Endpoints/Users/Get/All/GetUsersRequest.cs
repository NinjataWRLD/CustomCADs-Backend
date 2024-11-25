using CustomCADs.Account.Domain.Users.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Account.Endpoints.Users.Get.All;

public record GetUsersRequest(
    string? Name = default,
    UserSortingType SortingType = UserSortingType.CreationDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 50
);
