using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Orders.Application.CompletedOrders.Commands.Create;

public record CreateCompletedOrderCommand(
    string Name,
    string Description,
    bool Delivery,
    DateTime OrderDate,
    AccountId BuyerId,
    AccountId DesignerId,
    CadId CadId
) : ICommand<CompletedOrderId>;
