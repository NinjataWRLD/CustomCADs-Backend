using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders;

using static CompletedOrdersData;

public class CompletedOrdersBaseUnitTests
{
    public static CompletedOrder CreateOrder(
        string? name = null,
        string? description = null,
        decimal? price = null,
        bool? delivery = null,
        DateTime? orderDate = null,
        AccountId? buyerId = null,
        AccountId? designerId = null,
        CadId? cadId = null
)
        => CompletedOrder.Create(
            name: name ?? ValidName1,
            description: description ?? ValidDescription1,
            price: price ?? ValidPrice1,
            delivery: delivery ?? false,
            orderDate: orderDate ?? DateTime.UtcNow.AddDays(-1),
            buyerId: buyerId ?? ValidBuyerId1,
            designerId: designerId ?? ValidDesignerId1,
            cadId: cadId ?? ValidCadId1
        );

    public static CompletedOrder CreateOrderWithId(
        CompletedOrderId? id = null,
        string? name = null,
        string? description = null,
        decimal? price = null,
        bool? delivery = null,
        DateTime? orderDate = null,
        AccountId? buyerId = null,
        AccountId? designerId = null,
        CadId? cadId = null
    ) => CompletedOrder.CreateWithId(
            id: id ?? ValidId1,
            name: name ?? ValidName1,
            description: description ?? ValidDescription1,
            price: price ?? ValidPrice1,
            delivery: delivery ?? false,
            orderDate: orderDate ?? DateTime.UtcNow.AddDays(-1),
            buyerId: buyerId ?? ValidBuyerId1,
            designerId: designerId ?? ValidDesignerId1,
            cadId: cadId ?? ValidCadId1
        );
}
