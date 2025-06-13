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
	public static readonly string MinValidName = new('a', NameMinLength + 1);
	public static readonly string MaxValidName = new('a', NameMaxLength - 1);
	public const string InvalidName = "";
	public static readonly string MinInvalidName = new('a', NameMinLength - 1);
	public static readonly string MaxInvalidName = new('a', NameMaxLength + 1);

	public static readonly string MinValidDescription = new('a', DescriptionMinLength + 1);
	public static readonly string MaxValidDescription = new('a', DescriptionMaxLength - 1);
	public const string InvalidDescription = "";
	public static readonly string MinInvalidDescription = new('a', DescriptionMinLength - 1);
	public static readonly string MaxInvalidDescription = new('a', DescriptionMaxLength + 1);

	public const decimal ValidPrice = PriceMin + 1;
	public const decimal InvalidPrice = PriceMin - 1;

	public static readonly CustomId ValidId = CustomId.New();
	public static readonly AccountId ValidBuyerId = AccountId.New();
	public static readonly AccountId ValidDesignerId = AccountId.New();
	public static readonly CadId ValidCadId = CadId.New();
	public static readonly ShipmentId ValidShipmentId = ShipmentId.New();
	public static readonly CustomizationId ValidCustomizationId = CustomizationId.New();
}
