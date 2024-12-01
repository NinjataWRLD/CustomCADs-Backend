using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Queries.GetById;

public record GetOrderByIdQuery(
    OrderId Id,
    AccountId BuyerId
) : IQuery<GetOrderByIdDto>;
