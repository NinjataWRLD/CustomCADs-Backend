using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Orders.Application.CompletedOrders.Commands.Internal.Create;

public record CreateCompletedOrderCommand(
    string Name,
    string Description,
    decimal Price,
    bool Delivery,
    DateTime OrderDate,
    AccountId BuyerId,
    AccountId DesignerId,
    CadId CadId,
    CustomizationId? CustomizationId
) : ICommand<CompletedOrderId>;
