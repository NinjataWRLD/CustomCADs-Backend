using CustomCADs.Orders.Domain.OngoingOrders;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.UnitTests.Orders.Data;

using static OngoingOrderConstants;

public static class OngoingOrdersData
{
    public static readonly CancellationToken ct = CancellationToken.None;
    
    public static readonly OngoingOrderId ValidId1 = OngoingOrderId.New();
    public static readonly OngoingOrderId ValidId2 = OngoingOrderId.New();

    public static readonly string ValidName1 = new('a', NameMinLength + 1);
    public static readonly string ValidName2 = new('a', NameMaxLength - 1);
    public static readonly string InvalidName1 = new('a', NameMinLength - 1);
    public static readonly string InvalidName2 = new('a', NameMaxLength + 1);
    public const string InvalidName3 = "";

    public static readonly string ValidDescription1 = new('a', DescriptionMinLength + 1);
    public static readonly string ValidDescription2 = new('a', DescriptionMaxLength - 1);
    public static readonly string InvalidDescription1 = new('a', DescriptionMinLength - 1);
    public static readonly string InvalidDescription2 = new('a', DescriptionMaxLength + 1);
    public const string InvalidDescription3 = "";

    public const decimal ValidPrice1 = PriceMin + 1;
    public const decimal ValidPrice2 = PriceMax - 1;
    public const decimal InvalidPrice1 = PriceMin - 1;
    public const decimal InvalidPrice2 = PriceMax + 1;

    public static readonly AccountId ValidBuyerId1 = AccountId.New();
    public static readonly AccountId ValidBuyerId2 = AccountId.New();

    public static readonly AccountId ValidDesignerId1 = AccountId.New();
    public static readonly AccountId ValidDesignerId2 = AccountId.New();

    public static readonly CadId ValidCadId1 = CadId.New();
    public static readonly CadId ValidCadId2 = CadId.New();
}
