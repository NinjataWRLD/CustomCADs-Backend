using CustomCADs.Customs.Domain.Customs;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Customs.Data;

using static CustomConstants;

public static class CustomsData
{
	public static readonly CancellationToken ct = CancellationToken.None;

	public static readonly CustomId ValidId1 = CustomId.New();
	public static readonly CustomId ValidId2 = CustomId.New();

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

	public static readonly ShipmentId ValidShipmentId1 = ShipmentId.New();
	public static readonly ShipmentId ValidShipmentId2 = ShipmentId.New();

	public static readonly CustomizationId ValidCustomizationId1 = CustomizationId.New();
	public static readonly CustomizationId ValidCustomizationId2 = CustomizationId.New();
	public static readonly CustomId ValidId = CustomId.New();
	public static readonly AccountId ValidBuyerId = AccountId.New();
	public static readonly AccountId ValidDesignerId = AccountId.New();
	public static readonly CadId ValidCadId = CadId.New();
	public static readonly ShipmentId ValidShipmentId = ShipmentId.New();
	public static readonly CustomizationId ValidCustomizationId = CustomizationId.New();
}
