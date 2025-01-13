using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Orders.Domain.OngoingOrders;

using static OngoingOrdersData;

public class OngoingOrdersBaseUnitTests
{
    public static OngoingOrder CreateOrder(string? name = null, string? description = null, bool? delivery = null, AccountId? buyerId = null)
        => OngoingOrder.Create(
            name: name ?? ValidName1,
            description: description ?? ValidDescription1,
            delivery: delivery ?? false,
            buyerId: buyerId ?? ValidBuyerId1
        );

    public static OngoingOrder CreateOrderWithId(OngoingOrderId? id = null, string? name = null, string? description = null, bool? delivery = null, AccountId? buyerId = null)
        => OngoingOrder.CreateWithId(
            id: id ?? ValidId1,
            name: name ?? ValidName1,
            description: description ?? ValidDescription1,
            delivery: delivery ?? false,
            buyerId: buyerId ?? ValidBuyerId1
        );
}
