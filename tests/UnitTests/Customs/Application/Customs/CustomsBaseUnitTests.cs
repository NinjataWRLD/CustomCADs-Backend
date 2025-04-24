using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs;

using static CustomsData;

public class CustomsBaseUnitTests
{
    public static Custom CreateCustom(string? name = null, string? description = null, bool? forDelivery = null, AccountId? buyerId = null)
        => Custom.Create(
            name: name ?? ValidName1,
            description: description ?? ValidDescription1,
            forDelivery: forDelivery ?? false,
            buyerId: buyerId ?? ValidBuyerId1
        );

    public static Custom CreateCustomWithId(CustomId? id = null, string? name = null, string? description = null, bool? forDelivery = null, AccountId? buyerId = null)
        => Custom.CreateWithId(
            id: id ?? ValidId1,
            name: name ?? ValidName1,
            description: description ?? ValidDescription1,
            delivery: forDelivery ?? false,
            buyerId: buyerId ?? ValidBuyerId1
        );
}
