using CustomCADs.Account.Domain.Users.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Account.Domain.Users.ValueObjects;

public record UserSorting(
    UserSortingType Type = UserSortingType.UploadDate,
    SortingDirection Direction = SortingDirection.Descending
);
