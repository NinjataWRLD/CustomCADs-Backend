using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Catalog.Domain.Products;

using static ProductsData;

public class ProductsBaseUnitTests
{
    protected static Product CreateProduct(
        string? name = null,
        string? description = null,
        decimal? price = null,
        AccountId? creatorId = null,
        CategoryId? categoryId = null,
        ImageId? imageId = null,
        CadId? cadId = null
    ) => Product.Create(
            name: name ?? ValidName1,
            description: description ?? ValidDescription1,
            price: price ?? ValidPrice1,
            creatorId: creatorId ?? ValidCreatorId,
            categoryId: categoryId ?? ValidCategoryId,
            imageId: imageId ?? ValidImageId,
            cadId: cadId ?? ValidCadId
        );
}
