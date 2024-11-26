using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Queries.GetById;

public record GetOrderByIdQuery(
    OrderId Id,
    UserId BuyerId
) : IQuery<GetOrderByIdDto>;
