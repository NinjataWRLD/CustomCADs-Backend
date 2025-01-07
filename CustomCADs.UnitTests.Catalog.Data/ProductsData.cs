using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Catalog.Data;

using static ProductConstants;

public static class ProductsData
{
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

    public static readonly CancellationToken ct = CancellationToken.None;
    public static readonly ProductId ValidId = new(Guid.NewGuid());
    public static readonly AccountId ValidCreatorId = new(Guid.NewGuid());
    public static readonly AccountId ValidDesignerId = new(Guid.NewGuid());
    public static readonly CategoryId ValidCategoryId = new(0);
    public static readonly ImageId ValidImageId = new(Guid.NewGuid());
    public static readonly CadId ValidCadId = new(Guid.NewGuid());
}
