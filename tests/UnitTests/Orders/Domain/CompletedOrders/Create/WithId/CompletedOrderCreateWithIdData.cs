using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.WithId;

public class CompletedOrderCreateWithIdData : TheoryData<CompletedOrderId, string, string, decimal, bool, DateTimeOffset, AccountId>;
