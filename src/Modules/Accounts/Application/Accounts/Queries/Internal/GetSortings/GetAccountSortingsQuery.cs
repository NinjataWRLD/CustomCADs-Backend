using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetSortings;

[AddRequestCaching(ExpirationType.Absolute)]
public record GetAccountSortingsQuery : IQuery<string[]>;
