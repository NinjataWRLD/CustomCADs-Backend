using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Orders.Domain.CompletedOrders.Create.Normal;

public class CompletedOrderCreateData : TheoryData<string, string, decimal, bool, DateTime, AccountId>;
