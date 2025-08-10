using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Catalog.Data;

using static ProductConstants;

public static class ProductsData
{
	public static readonly string MinValidName = new('a', NameMinLength + 1);
	public static readonly string MaxValidName = new('a', NameMaxLength - 1);
	public static readonly string MinInvalidName = new('a', NameMinLength - 1);
	public static readonly string MaxInvalidName = new('a', NameMaxLength + 1);

	public static readonly string MinValidDescription = new('a', DescriptionMinLength + 1);
	public static readonly string MaxValidDescription = new('a', DescriptionMaxLength - 1);
	public static readonly string MinInvalidDescription = new('a', DescriptionMinLength - 1);
	public static readonly string MaxInvalidDescription = new('a', DescriptionMaxLength + 1);

	public const decimal MinValidPrice = PriceMin + 1;
	public const decimal MaxValidPrice = PriceMax - 1;
	public const decimal MinInvalidPrice = PriceMin - 1;
	public const decimal MaxInvalidPrice = PriceMax + 1;

	public static readonly ProductId ValidId = ProductId.New();
	public static readonly AccountId ValidCreatorId = AccountId.New();
	public static readonly AccountId ValidDesignerId = AccountId.New();
	public static readonly CategoryId ValidCategoryId = CategoryId.New();
	public static readonly ImageId ValidImageId = ImageId.New();
	public static readonly CadId ValidCadId = CadId.New();
}
