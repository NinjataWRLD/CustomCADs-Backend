using CustomCADs.Accounts.Domain.Accounts.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Accounts.Endpoints.Accounts.Get.All;

public record GetAccountsRequest(
    string? Name = default,
    AccountSortingType SortingType = AccountSortingType.CreationDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 50
);
