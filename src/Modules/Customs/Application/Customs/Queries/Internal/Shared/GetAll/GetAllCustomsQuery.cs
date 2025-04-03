using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Customs.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;

public sealed record GetAllCustomsQuery(
    Pagination Pagination,
    CustomStatus? CustomStatus = null,
    AccountId? BuyerId = null,
    AccountId? DesignerId = null,
    bool? ForDelivery = null,
    string? Name = null,
    CustomSorting? Sorting = null
) : IQuery<Result<GetAllCustomsDto>>;
