using CustomCADs.Account.Domain.Users.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Account.Domain.Users.ValueObjects;

public record UserSorting(
    UserSortingType Type = UserSortingType.CreationDate,
    SortingDirection Direction = SortingDirection.Descending
);
