using CustomCADs.Accounts.Domain.Accounts.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Accounts.Domain.Accounts.ValueObjects;

public record AccountSorting(
	AccountSortingType Type = AccountSortingType.CreationDate,
	SortingDirection Direction = SortingDirection.Descending
);
