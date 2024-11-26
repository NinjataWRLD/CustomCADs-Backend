using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Queries.IsBuyer;

public record IsOrderBuyerQuery(OrderId Id, UserId UserId) : IQuery<bool>;
