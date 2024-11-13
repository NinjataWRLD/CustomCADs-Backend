using CustomCADs.Account.Domain.Users.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Account.Endpoints.Users.GetUsers;

public record GetUsersRequest(
    string? Name = default,
    UserSortingType SortingType = UserSortingType.CreationDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 50
);
