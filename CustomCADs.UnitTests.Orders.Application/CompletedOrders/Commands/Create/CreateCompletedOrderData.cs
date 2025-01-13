using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Create;

public class CreateCompletedOrderData : TheoryData<string, string, bool, DateTime, AccountId, AccountId, CadId>;
