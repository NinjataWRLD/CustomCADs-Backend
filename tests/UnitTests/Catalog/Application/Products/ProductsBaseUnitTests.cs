using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Catalog.Application.Products;

using static ProductsData;

public class ProductsBaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	protected static Product CreateProduct(
		string? name = null,
		string? description = null,
		decimal? price = null,
		AccountId? creatorId = null,
		CategoryId? categoryId = null,
		ImageId? imageId = null,
		CadId? cadId = null
	) => Product.Create(
			name: name ?? MinValidName,
			description: description ?? MinValidDescription,
			price: price ?? MinValidPrice,
			creatorId: creatorId ?? ValidCreatorId,
			categoryId: categoryId ?? ValidCategoryId,
			imageId: imageId ?? ValidImageId,
			cadId: cadId ?? ValidCadId
		);

	protected static Product CreateProductWithId(
		string? name = null,
		string? description = null,
		decimal? price = null,
		AccountId? creatorId = null,
		CategoryId? categoryId = null,
		ImageId? imageId = null,
		CadId? cadId = null,
		ProductId? id = null,
		DateTimeOffset? uploadedAt = null
	) => Product.CreateWithId(
			name: name ?? MinValidName,
			description: description ?? MinValidDescription,
			price: price ?? MinValidPrice,
			creatorId: creatorId ?? ValidCreatorId,
			categoryId: categoryId ?? ValidCategoryId,
			imageId: imageId ?? ValidImageId,
			cadId: cadId ?? ValidCadId,
			id: id,
			uploadedAt: uploadedAt
		);
}
