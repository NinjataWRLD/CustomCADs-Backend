using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Application.Orders.Queries.IsBuyer;

public record IsOrderBuyerQuery(OrderId Id, UserId UserId) : IQuery<bool>;
