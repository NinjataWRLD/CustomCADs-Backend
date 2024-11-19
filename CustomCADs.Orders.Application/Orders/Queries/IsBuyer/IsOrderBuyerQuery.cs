using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Orders.Queries.IsBuyer;

public record IsOrderBuyerQuery(OrderId Id, UserId UserId) : IQuery<bool>;
