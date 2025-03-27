using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Internal.Create;

public class CreateCompletedOrderData : TheoryData<string, string, decimal, bool, DateTimeOffset, AccountId, AccountId, CadId, CustomizationId?>;
