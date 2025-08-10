using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Domain.Customs;

using static CustomsData;

public class CustomsBaseUnitTests
{
	public static Custom CreateCustom(string? name = null, string? description = null, bool? forDelivery = null, AccountId? buyerId = null)
		=> Custom.Create(
			name: name ?? MinValidName,
			description: description ?? MinValidDescription,
			forDelivery: forDelivery ?? false,
			buyerId: buyerId ?? ValidBuyerId
		);

	public static Custom CreateCustomWithId(CustomId? id = null, string? name = null, string? description = null, bool? delivery = null, AccountId? buyerId = null)
		=> Custom.CreateWithId(
			id: id ?? ValidId,
			name: name ?? MinValidName,
			description: description ?? MinValidDescription,
			delivery: delivery ?? false,
			buyerId: buyerId ?? ValidBuyerId
		);
}
